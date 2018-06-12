using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IntegrationEvents.Models;
using IntegrationEvents.Models.Entities;

namespace IntegrationEvents.Controllers
{
    public class TrainingCoursesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TrainingCourses
        public IQueryable<TrainingCourse> GetCourses()
        {
            return db.Courses.Include(c => c.RegisteredStudents);
        }

        // GET: api/TrainingCourses/5
        [ResponseType(typeof(TrainingCourse))]
        public IHttpActionResult GetTrainingCourse(int id)
        {
            TrainingCourse trainingCourse = db.Courses.Find(id);
            if (trainingCourse == null)
            {
                return NotFound();
            }

            return Ok(trainingCourse);
        }

        // PUT: api/TrainingCourses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrainingCourse(int id, TrainingCourse trainingCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingCourse.Id)
            {
                return BadRequest();
            }

            db.Entry(trainingCourse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingCourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TrainingCourses
        [ResponseType(typeof(TrainingCourse))]
        public IHttpActionResult PostTrainingCourse(TrainingCourse trainingCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courses.Add(trainingCourse);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trainingCourse.Id }, trainingCourse);
        }

        [ResponseType(typeof(TrainingCourse))]
        public IHttpActionResult PostRegisterStudent(int id, int studentId)
        {
            TrainingCourse trainingCourse = db.Courses
                .Include(tc => tc.RegisteredStudents)
                .FirstOrDefault(tc => tc.Id == id);

            if (trainingCourse == null)
            {
                return NotFound();
            }

            Student student = db.Students.Find(studentId);
            if (student == null)
            {
                return NotFound();
            }
            trainingCourse.Register(student);
            db.SaveChanges();
            return Ok(trainingCourse);
        }

        // DELETE: api/TrainingCourses/5
        [ResponseType(typeof(TrainingCourse))]
        public IHttpActionResult DeleteTrainingCourse(int id)
        {
            TrainingCourse trainingCourse = db.Courses.Find(id);
            if (trainingCourse == null)
            {
                return NotFound();
            }

            db.Courses.Remove(trainingCourse);
            db.SaveChanges();

            return Ok(trainingCourse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainingCourseExists(int id)
        {
            return db.Courses.Count(e => e.Id == id) > 0;
        }
    }
}