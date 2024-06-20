using ContactManagementSystemAPI.IRepository;
using ContactManagementSystemAPI.Models;
using ContactManagementSystemAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        [HttpGet("GetById")]
        public ActionResult GetById(string Id)
        {
            var repo = new ContactJsonRepository();
            var data = repo.GetById(Id);
            if (data != null)
            {
                return Ok(new JsonResult(new { StatusCode = 200, Data = data }));
            }
            else
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = "Record not found!" }));
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var repo = new ContactJsonRepository();
            var data = repo.GetAll();
            if (data != null)
            {
                return Ok(new JsonResult(new { StatusCode = 200, Data = data }));
            }
            else
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = "Record not found!" }));
            }
        }

        [HttpGet("Search")]
        public ActionResult Search(string text)
        {
            var repo = new ContactJsonRepository();
            var data = repo.Search(text);
            if (data != null)
            {
                return Ok(new JsonResult(new { StatusCode = 200, Data = data }));
            }
            else
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = "Record not found!" }));
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string Id)
        {
            try
            {
                var repo = new ContactJsonRepository();
                var result = repo.Delete(Id);
                //var data = repo.Commit();
                if (result == true)
                    return Ok(new JsonResult(new { StatusCode = 200, Message = "Record deleted successfully!" }));
                else
                    return BadRequest(new JsonResult(new { StatusCode = 400, Message = "Record not found!" }));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = ex.Message }));
            }
        }

        [HttpPost("Add")]
        public ActionResult Add(Contact data)
        {
            try
            {
                var repo = new ContactJsonRepository();
                repo.Add(data);
                //repo.Commit();
                return Ok(new JsonResult(new { StatusCode = 200, Data = data }));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = ex.Message }));
            }
        }

        [HttpPost("AddRange")]
        public ActionResult AddRange(List<Contact> data)
        {
            try
            {
                var repo = new ContactJsonRepository();
                repo.AddRange(data);
                //repo.Commit();
                return Ok(new JsonResult(new { StatusCode = 200, Data = data }));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = ex.Message }));
            }
        }

        [HttpPost("Update")]
        public ActionResult Update(Contact data)
        {
            try
            {
                var repo = new ContactJsonRepository();
                repo.Update(data);
               // repo.Commit();
                return Ok(new JsonResult(new { StatusCode = 200, Data = data }));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = ex.Message }));
            }
        }
    }
}
