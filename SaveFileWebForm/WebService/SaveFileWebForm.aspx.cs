using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaveFileWebService.WebService
{
    public partial class SaveFileWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //得到客户端上传的文件
                    HttpPostedFile file = Request.Files[0];
                    //服务器端要保存的路径
                    string path = System.AppDomain.CurrentDomain.BaseDirectory;
                    string p = path.Substring(0, path.LastIndexOf("SaveFileWebForm"));
                    string filePath = p + "Web_Servers\\bin\\Debug\\net6.0-windows\\127.1.0.0\\" + file.FileName;
                    file.SaveAs(filePath);
                    //返回结果
                    Response.Write("Success");
                }
                catch
                {
                    Response.Write("Error");
                }
            }
            else
            {
                
                Response.Write("Please upload your file");
            }
        }
    }
}