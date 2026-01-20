using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserRepository repository, ILogger<UsersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// GET: Retrieve all users
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                // FIX: Added try-catch block to handle potential errors
                var users = _repository.GetAllUsers();
                _logger.LogInformation("Retrieved {Count} users", users.Count);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users");
                throw;
            }
        }

        /// <summary>
        /// GET: Retrieve a specific user by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            try
            {
                var user = _repository.GetUserById(id);
                
                // FIX: Added null check to handle non-existent users
                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found", id);
                    return NotFound(new { error = $"User with ID {id} not found" });
                }

                _logger.LogInformation("Retrieved user with ID {Id}", id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// POST: Add a new user
        /// </summary>
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            try
            {
                // FIX: Added validation check to ensure only valid data is processed
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid user data provided");
                    return BadRequest(ModelState);
                }

                var createdUser = _repository.AddUser(user);
                _logger.LogInformation("Created new user with ID {Id}", createdUser.Id);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        /// <summary>
        /// PUT: Update an existing user's details
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                // FIX: Added validation check
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid user data provided for update");
                    return BadRequest(ModelState);
                }

                var updatedUser = _repository.UpdateUser(id, user);
                
                // FIX: Added null check for non-existent users
                if (updatedUser == null)
                {
                    _logger.LogWarning("User with ID {Id} not found for update", id);
                    return NotFound(new { error = $"User with ID {id} not found" });
                }

                _logger.LogInformation("Updated user with ID {Id}", id);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// DELETE: Remove a user by ID
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var result = _repository.DeleteUser(id);
                if (!result)
                {
                    _logger.LogWarning("User with ID {Id} not found for deletion", id);
                    return NotFound(new { error = $"User with ID {id} not found" });
                }

                _logger.LogInformation("Deleted user with ID {Id}", id);
                return Ok(new { message = $"User with ID {id} successfully deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {Id}", id);
                throw;
            }
        }
    }
}
