using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ControlLazerApp
{
    public class OrderController:ApiController
    {
        [Route("PostStatus")]
        [HttpPost]
        public IHttpActionResult PostStatus([FromBody] OrderInfor data) 
        {
            if(data != null) 
            {
                Form1.OpenCutingFile(data.File_name, data.Color);
                return Ok();
            }
            else 
            {
                return BadRequest("Null Data");
            }
        }
    }

    public class OrderInfor 
    {
        public string MO_Code { get; set; }
        public string LP_Code { get; set; }
        public string File_name { get; set; }
        public string Cutting_Code { get; set; }
        public string Machine { get; set; }
        public string Color { get; set; }
        public string Quantity { get; set; }
    }
}
