﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalizationServer;
using LocalizationServer.Data;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace LocalizationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly LocalizationServerContext _context;

        //Localization
        private readonly ResourceManager _resourceManager;
        private readonly IStringLocalizer<StudentsController> _localizer;
        private readonly ILogger _logger;

        public StudentsController(LocalizationServerContext context,
                                   ResourceManager resourceManager,
                                   IStringLocalizer<StudentsController> localizer,
                                   ILogger<StudentsController> logger)
        {
            _context = context;

            //Localization
            _resourceManager = resourceManager;
            _localizer = localizer;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            _logger.LogInformation(BuildLogInfo(nameof(GetStudent), "LoggingGetStudents"));
            return await _context.Student.ToListAsync();
        }

        // GET: api/Students/5
        // GET: https://localhost:44387/api/Students/4000?culture=fr-FR
        // GET: https://localhost:44387/api/Students/4000?culture=es
        // GET: https://localhost:44387/api/Students/4000?culture=en-US
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            _logger.LogInformation(BuildLogInfo(nameof(GetStudent), "LoggingGetStudent", id));

            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                var resourceManager = HttpContext.RequestServices.GetService(typeof(ResourceManager)) as ResourceManager;

                _logger.LogError(BuildLogInfo(nameof(GetStudent), "StudentNotFound", id));

                //return NotFound();
                return new NotFoundObjectResult(_localizer["StudentNotFound", id].Value);
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            student.EnrollDate = DateTime.UtcNow;
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        // DELETE: https://localhost:44387/api/Students/4000?culture=fr-FR
        // DELETE: https://localhost:44387/api/Students/4000?culture=es
        // DELETE: https://localhost:44387/api/Students/4000?culture=en-US
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            _logger.LogInformation(BuildLogInfo(nameof(DeleteStudent), "LoggingDeletingStudent", id));

            var student = await _context.Student.FindAsync(id);

            Console.WriteLine(student);

            if (student == null)
            {
                _logger.LogError(BuildLogInfo(nameof(DeleteStudent), "StudentNotFound", id));
                return new NotFoundObjectResult(_localizer["StudentNotFound", id].Value);
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }

        private string BuildLogInfo(string methodName, string resourceStringName, params object[] replacements)
        {
            return $"{methodName}: {_localizer[resourceStringName, replacements]}";
        }

        private string BuildStringFromResource(string resourceStringName, params object[] replacements)
        {
            return string.Format(_resourceManager.GetString(resourceStringName), replacements);
        }
    }
}
