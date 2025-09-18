using System;
using Microsoft.AspNetCore.Mvc;
using VulnerableCoreApp.ViewModels;
using VulnerableCoreApp.Models;
using VulnerableCoreApp.Repository;

namespace VulnerableCoreApp.Controllers
        // 11. SQL Injection with direct DB call (simulated)
        [HttpPost]
        public IActionResult RealSqlInjection(string username)
        {
            string query = $"SELECT * FROM Users WHERE Username = '{username}'";
            // Simulate DB call
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query);
            ViewBag.Query = query;
            return View();
        }

        // 12. Buffer overflow (simulated, unsafe code)
        [HttpGet]
        public IActionResult BufferOverflow(string input)
        {
            unsafe
            {
                fixed (char* p = input)
                {
                    char buffer[5];
                    for (int i = 0; i < input.Length; i++)
                        buffer[i] = p[i]; // overflow if input.Length > 5
                }
            }
            return View();
        }

        // 13. Null pointer dereference
        [HttpGet]
        public IActionResult NullDereference()
        {
            string s = null;
            int len = s.Length; // null dereference
            ViewBag.Len = len;
            return View();
        }

        // 14. Memory leak (simulated)
        [HttpGet]
        public IActionResult MemoryLeak()
        {
            var list = new System.Collections.Generic.List<byte[]>();
            for (int i = 0; i < 1000000; i++)
                list.Add(new byte[1024]); // never released
            ViewBag.Count = list.Count;
            return View();
        }

        // 15. Unhandled exception
        [HttpGet]
        public IActionResult UnhandledException(string input)
        {
            int x = int.Parse(input); // may throw
            ViewBag.X = x;
            return View();
        }

        // 16. Hardcoded cryptographic key
        [HttpGet]
        public IActionResult HardcodedKey()
        {
            string key = "1234567890123456";
            ViewBag.Key = key;
            return View();
        }

        // 17. Use of insecure protocol
        [HttpGet]
        public IActionResult InsecureProtocol()
        {
            string url = "http://example.com";
            System.Net.WebClient client = new System.Net.WebClient();
            string data = client.DownloadString(url);
            ViewBag.Data = data;
            return View();
        }

        // 18. Unvalidated redirect
        [HttpGet]
        public IActionResult UnvalidatedRedirect(string url)
        {
            Response.Redirect(url); // unsafe
            return View();
        }

        // 19. Use of deprecated API
        [HttpGet]
        public IActionResult DeprecatedApi()
        {
            System.Net.WebClient client = new System.Net.WebClient(); // deprecated
            ViewBag.Client = client.ToString();
            return View();
        }

        // 20. Unused variable (quality defect)
        [HttpGet]
        public IActionResult UnusedVariable()
        {
            int unused = 42;
            return View();
        }

        // 21. Unreachable code
        [HttpGet]
        public IActionResult UnreachableCode()
        {
            return View();
            int x = 1; // unreachable
        }

        // 22. Empty catch block (reliability)
        [HttpGet]
        public IActionResult EmptyCatch()
        {
            try
            {
                int x = int.Parse("abc");
            }
            catch { }
            return View();
        }

        // 23. Magic number (coding standard)
        [HttpGet]
        public IActionResult MagicNumber()
        {
            int timeout = 1234; // magic number
            ViewBag.Timeout = timeout;
            return View();
        }

        // 24. Uninitialized variable
        [HttpGet]
        public IActionResult UninitializedVariable()
        {
            int x;
            ViewBag.X = x; // use before assignment
            return View();
        }

        // 25. Division by zero
        [HttpGet]
        public IActionResult DivisionByZero(int y)
        {
            int z = 10 / y;
            ViewBag.Z = z;
            return View();
        }

        // 26. Use of insecure random
        [HttpGet]
        public IActionResult InsecureRandom2()
        {
            var rand = new Random();
            int val = rand.Next();
            ViewBag.Random = val;
            return View();
        }

        // 27. Use of weak hash
        [HttpGet]
        public IActionResult WeakHash(string input)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            ViewBag.Hash = Convert.ToBase64String(hash);
            return View();
        }

        // 28. Use of insecure XML parser
        [HttpPost]
        public IActionResult InsecureXml(string xml)
        {
            var doc = new System.Xml.XmlDocument();
            doc.LoadXml(xml); // unsafe
            ViewBag.Xml = doc.InnerXml;
            return View();
        }

        // 29. Use of insecure cookie
        [HttpGet]
        public IActionResult InsecureCookie()
        {
            Response.Cookies.Append("session", "12345"); // no secure flag
            return View();
        }

        // 30. Use of insecure HTTP header
        [HttpGet]
        public IActionResult InsecureHeader()
        {
            Response.Headers.Add("X-Powered-By", "ASP.NET"); // info leak
            return View();
        }
{
    public class CrossSiteScriptingController : Controller
        // Vulnerable: SQL Injection
        [HttpPost]
        public IActionResult AnotherSqlInjection(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
            // Simulate DB call
            ViewBag.Query = query;
            return View();
        }

        // Vulnerable: Hardcoded credentials
        [HttpGet]
        public IActionResult AnotherHardcodedCredentials()
        {
            string user = "root";
            string pass = "toor";
            ViewBag.Credentials = user + ":" + pass;
            return View();
        }

        // Vulnerable: Null pointer dereference
        [HttpGet]
        public IActionResult AnotherNullDereference()
        {
            string s = null;
            int len = s.Length; // null dereference
            ViewBag.Len = len;
            return View();
        }
    {
        // 1. Reflected XSS
        [HttpGet]
        public IActionResult ReflectedXSS(string input)
        {
            // Echo user input directly (unsafe)
            ViewBag.UserInput = input;
            return View();
        }

        // 2. Simulated SQL Injection
        [HttpGet]
        public IActionResult SqlInjection(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
            ViewBag.Query = query;
            return View();
        }

        // 3. Hardcoded credentials
        [HttpGet]
        public IActionResult HardcodedCredentials()
        {
            string user = "admin";
            string pass = "password123";
            ViewBag.Credentials = user + ":" + pass;
            return View();
        }

        // 4. Insecure deserialization
        [HttpPost]
        public IActionResult InsecureDeserialization(string data)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(data); // unsafe
            ViewBag.Deserialized = obj;
            return View();
        }

        // 5. Use of weak cryptography
        [HttpGet]
        public IActionResult WeakCrypto(string input)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            ViewBag.Hash = Convert.ToBase64String(hash);
            return View();
        }

        // 6. Path traversal
        [HttpGet]
        public IActionResult PathTraversal(string filename)
        {
            string path = "/tmp/" + filename;
            string content = System.IO.File.ReadAllText(path); // unsafe
            ViewBag.FileContent = content;
            return View();
        }

        // 7. Insecure random number generation
        [HttpGet]
        public IActionResult InsecureRandom()
        {
            var rand = new Random();
            int val = rand.Next();
            ViewBag.Random = val;
            return View();
        }

        // 8. Information leakage via error messages
        [HttpGet]
        public IActionResult InfoLeak(string input)
        {
            try
            {
                int x = int.Parse(input);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString(); // leaks stack trace
            }
            return View();
        }

        // 9. Open redirect
        [HttpGet]
        public IActionResult OpenRedirect(string url)
        {
            return Redirect(url); // unsafe
        }

        // 10. Use of deprecated/unsafe API
        [HttpGet]
        public IActionResult UnsafeApi()
        {
            string input = "test";
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(input); // ASCII is unsafe for non-ASCII data
            ViewBag.Bytes = bytes.Length;
            return View();
        }
        private ICommentsRepository commentsRepository;
        public CrossSiteScriptingController(ICommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DemoTypeI()
        {
            CommentsViewModel comments = commentsRepository.GetAll();
            
            return View(comments);
        }

        [HttpPost]
        public IActionResult DemoTypeI(CommentViewModel comment)
        {
            Comment newComment = new Comment();
            newComment.ID = Guid.NewGuid().ToString();
            newComment.Username = "Anonymous";
            newComment.CreatedAt = DateTime.Now;
            newComment.Text = comment.Text;
            commentsRepository.Save(newComment);

            return RedirectToAction("DemoTypeI");
        }

        [HttpPost]
        public IActionResult DemoTypeIDelete(String ID)
        {
            commentsRepository.Delete(ID);

            return RedirectToAction("DemoTypeI");
        }

        [HttpGet]
        public IActionResult DemoTypeII(string query)
        {
            ViewData["query"] = query;
            
            // Disable Chrome browser XSS protection 
            HttpContext.Response.Headers.Add("X-XSS-Protection","0");
            return View();
        }
    }
}