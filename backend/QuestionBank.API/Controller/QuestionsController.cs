using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Questionbanknew.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]   // 👈 Only authenticated users can access
public class QuestionsController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    public QuestionsController(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("DefaultConnection")!;
    }

    // ✅ 1. Add Question
    [HttpPost("add")]
    public IActionResult AddQuestion([FromBody] QuestionModel request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
            return Unauthorized(ApiResponse.Fail("Unauthorized"));

        request.CreatedBy = int.Parse(userIdClaim.Value);

        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var transaction = conn.BeginTransaction();

        try
        {
            // Insert Question
            var sqlQ = @"INSERT INTO Questions (Subject, QuestionText, CorrectAnswer, CreatedBy)
                         OUTPUT INSERTED.QuestionID
                         VALUES (@Subject, @QuestionText, @CorrectAnswer, @CreatedBy)";
            int questionId;
            using (var cmd = new SqlCommand(sqlQ, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Subject", request.Subject);
                cmd.Parameters.AddWithValue("@QuestionText", request.QuestionText);
                cmd.Parameters.AddWithValue("@CorrectAnswer", request.CorrectAnswer);
                cmd.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                questionId = (int)cmd.ExecuteScalar();
            }

            // Insert Options
            foreach (var opt in request.Options)
            {
                var sqlO = @"INSERT INTO Options (QuestionID, OptionLabel, OptionText)
                             VALUES (@QuestionID, @OptionLabel, @OptionText)";
                using var cmd = new SqlCommand(sqlO, conn, transaction);
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                cmd.Parameters.AddWithValue("@OptionLabel", opt.OptionLabel);
                cmd.Parameters.AddWithValue("@OptionText", opt.OptionText);
                cmd.ExecuteNonQuery();
            }

            transaction.Commit();
            return Ok(ApiResponse.Ok("Question added successfully", new { questionId }));
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return StatusCode(500, ApiResponse.Fail("Failed to add question", ex.Message));
        }
    }

    // ✅ 2. Get All Questions
    [HttpGet]
    public IActionResult GetAllQuestions()
    {
        try
        {
            var questions = new List<QuestionModel>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT q.QuestionID, q.Subject, q.QuestionText, q.CorrectAnswer, q.CreatedBy,
                               o.OptionLabel, o.OptionText
                        FROM Questions q
                        LEFT JOIN Options o ON q.QuestionID = o.QuestionID";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            var dict = new Dictionary<int, QuestionModel>();

            while (reader.Read())
            {
                int qid = reader.GetInt32(0);

                if (!dict.ContainsKey(qid))
                {
                    dict[qid] = new QuestionModel
                    {
                        QuestionID = qid,
                        Subject = reader.GetString(1),
                        QuestionText = reader.GetString(2),
                        CorrectAnswer = reader.GetString(3),
                        CreatedBy = reader.GetInt32(4),
                        Options = new List<OptionModel>()
                    };
                }

                if (!reader.IsDBNull(5))
                {
                    dict[qid].Options.Add(new OptionModel
                    {
                        OptionLabel = reader.GetString(5),
                        OptionText = reader.GetString(6)
                    });
                }
            }

            questions = dict.Values.ToList();
            return Ok(ApiResponse.Ok("Questions fetched successfully", questions));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse.Fail("Failed to fetch questions", ex.Message));
        }
    }

    // ✅ 3. Get Question By Id
    [HttpGet("{id}")]
    public IActionResult GetQuestionById(int id)
    {
        try
        {
            QuestionModel? question = null;

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT q.QuestionID, q.Subject, q.QuestionText, q.CorrectAnswer, q.CreatedBy,
                               o.OptionLabel, o.OptionText
                        FROM Questions q
                        LEFT JOIN Options o ON q.QuestionID = o.QuestionID
                        WHERE q.QuestionID = @QuestionID";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@QuestionID", id);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (question == null)
                {
                    question = new QuestionModel
                    {
                        QuestionID = reader.GetInt32(0),
                        Subject = reader.GetString(1),
                        QuestionText = reader.GetString(2),
                        CorrectAnswer = reader.GetString(3),
                        CreatedBy = reader.GetInt32(4),
                        Options = new List<OptionModel>()
                    };
                }

                if (!reader.IsDBNull(5))
                {
                    question.Options.Add(new OptionModel
                    {
                        OptionLabel = reader.GetString(5),
                        OptionText = reader.GetString(6)
                    });
                }
            }

            if (question == null)
                return NotFound(ApiResponse.Fail("Question not found"));

            return Ok(ApiResponse.Ok("Question fetched successfully", question));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse.Fail("Failed to fetch question", ex.Message));
        }
    }

    // ✅ 4. Update Question
    [HttpPut("{id}")]
    public IActionResult UpdateQuestion(int id, [FromBody] QuestionModel request)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var transaction = conn.BeginTransaction();

        try
        {
            var sqlQ = @"UPDATE Questions
                         SET Subject = @Subject, QuestionText = @QuestionText, CorrectAnswer = @CorrectAnswer
                         WHERE QuestionID = @QuestionID";
            using (var cmd = new SqlCommand(sqlQ, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Subject", request.Subject);
                cmd.Parameters.AddWithValue("@QuestionText", request.QuestionText);
                cmd.Parameters.AddWithValue("@CorrectAnswer", request.CorrectAnswer);
                cmd.Parameters.AddWithValue("@QuestionID", id);
                cmd.ExecuteNonQuery();
            }

            var sqlDel = "DELETE FROM Options WHERE QuestionID = @QuestionID";
            using (var cmd = new SqlCommand(sqlDel, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@QuestionID", id);
                cmd.ExecuteNonQuery();
            }

            foreach (var opt in request.Options)
            {
                var sqlO = @"INSERT INTO Options (QuestionID, OptionLabel, OptionText)
                             VALUES (@QuestionID, @OptionLabel, @OptionText)";
                using var cmd = new SqlCommand(sqlO, conn, transaction);
                cmd.Parameters.AddWithValue("@QuestionID", id);
                cmd.Parameters.AddWithValue("@OptionLabel", opt.OptionLabel);
                cmd.Parameters.AddWithValue("@OptionText", opt.OptionText);
                cmd.ExecuteNonQuery();
            }

            transaction.Commit();
            return Ok(ApiResponse.Ok("Question updated successfully"));
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return StatusCode(500, ApiResponse.Fail("Failed to update question", ex.Message));
        }
    }

    // ✅ 5. Delete Question
    [HttpDelete("{id}")]
    public IActionResult DeleteQuestion(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var transaction = conn.BeginTransaction();

        try
        {
            var sqlOpt = "DELETE FROM Options WHERE QuestionID = @QuestionID";
            using (var cmd = new SqlCommand(sqlOpt, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@QuestionID", id);
                cmd.ExecuteNonQuery();
            }

            var sqlQ = "DELETE FROM Questions WHERE QuestionID = @QuestionID";
            using (var cmd = new SqlCommand(sqlQ, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@QuestionID", id);
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    return NotFound(ApiResponse.Fail("Question not found"));
            }

            transaction.Commit();
            return Ok(ApiResponse.Ok("Question deleted successfully"));
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return StatusCode(500, ApiResponse.Fail("Failed to delete question", ex.Message));
        }
    }
}
