using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P6BusStation_API_CristoferMontero.Models;
using P6BusStation_API_CristoferMontero.ModelsDTOs;

namespace P6BusStation_API_CristoferMontero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly P620242ticketBusContext _context;

        public UsersController(P620242ticketBusContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //este get muestra info del usuario a partir del correo
        //esto sirve para el login cargue info usuario - luego de ingresar
        [HttpGet("GetUserInfoByEmail")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetUserInfoByEmail(string pEmail)
        {
            //imitamos consulta de SSMS pero usando Linq
            var query = (from u in _context.Users
                         join ur in _context.UserRoles
                         on u.UserRoleId equals ur.UserRoleId
                         where u.Email == pEmail
                         select new
                         {
                             id = u.UserId,
                             correo = u.Email,
                             nombre = u.UserName,
                             telefono = u.PhoneNumber,
                             contrasennia = u.UserPassword,
                             direccion = u.Adress,
                             rolid = u.UserRoleId,
                             descriprol = ur.UserRoleDescription
                         }
                         ).ToList();


            //objeto en lista del tipo DTO de respuesta 
            //para llenar con datos de consulta
            List<UsuarioDTO> list = new List<UsuarioDTO>();

            //foreach para hacer recorrido 

            foreach (var item in query)
            {
                UsuarioDTO nuevoUsuario = new UsuarioDTO()
                {
                    UsuarioID = item.id,
                    Correo = item.correo,
                    Nombre = item.nombre,
                    Telefono = item.telefono,
                    Contrasennia = item.contrasennia,
                    Direccion = item.direccion,
                    RolID = item.rolid,
                    RolDescripcion = item.descriprol
                };

                list.Add(nuevoUsuario);
            }
            if (list == null) { return NotFound(); }

            return list;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users/ValidateUser
        [HttpPost("ValidateUser")]
        public async Task<ActionResult<UsuarioDTO>> ValidateUser([FromBody] UserLoginRequest loginRequest)
        {
            // Consulta para verificar si el usuario existe y las credenciales coinciden
            var user = await _context.Users
                .Where(u => u.Email == loginRequest.Email && u.UserPassword == loginRequest.Password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                // Si no se encuentra el usuario, retornamos Unauthorized
                return Unauthorized(new { message = "Credenciales incorrectas." });
            }

            // Si el usuario existe, mapeamos los datos a un DTO y retornamos la información
            UsuarioDTO usuarioDTO = new UsuarioDTO
            {
                UsuarioID = user.UserId,
                Correo = user.Email,
                Nombre = user.UserName,
                Telefono = user.PhoneNumber,
                Contrasennia = user.UserPassword,
                Direccion = user.Adress,
                RolID = user.UserRoleId
            };

            return Ok(usuarioDTO);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        //POST: De ingreso desde la APP con DTO
        [HttpPost("AddUserFromApp")]
        public async Task<ActionResult<UsuarioDTO>> AddUserFromApp(UsuarioDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //mapeo manual para transfomar DTO a modelo nativo
            User NuevoUsuarioNativo = new()
            {
                UserRoleId = user.RolID,
                Email = user.Correo,
                UserName = user.Nombre,
                PhoneNumber = user.Telefono,
                UserPassword = user.Contrasennia,
                Adress = user.Direccion,                
                UserRole = null
            };

            _context.Users.Add(NuevoUsuarioNativo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = NuevoUsuarioNativo.UserId }, NuevoUsuarioNativo);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
