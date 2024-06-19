using ContactManagementSystemAPI.IRepository;
using ContactManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository repo;
        public ContactController(IContactRepository _repo)
        {
            this.repo = _repo;
        }

        [HttpGet("GetById")]
        public ActionResult GetById(int Id)
        {
            var data = this.repo.GetById(Id);
            if (data != null)
            {
                return Ok(new JsonResult(new { StatusCode = 200, Data = data.Result }));
            }
            else
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = "Record not found!" }));
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var data = this.repo.GetAll();
            if (data != null)
            {
                return Ok(new JsonResult(new { StatusCode = 200, Data = data.Result }));
            }
            else
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = "Record not found!" }));
            }
        }

        [HttpGet("Search")]
        public ActionResult Search(string text)
        {
            var data = this.repo.Search(text);
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
        public ActionResult Delete(int Id)
        {
            try
            {
                this.repo.Delete(Id);
                var data = this.repo.Commit();
                if (data.Result > 0)
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
                this.repo.Add(data);
                this.repo.Commit();
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
                this.repo.AddRange(data);
                this.repo.Commit();
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
                this.repo.Update(data);
                this.repo.Commit();
                return Ok(new JsonResult(new { StatusCode = 200, Data = data }));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResult(new { StatusCode = 400, Message = ex.Message }));
            }
        }
    }
}
