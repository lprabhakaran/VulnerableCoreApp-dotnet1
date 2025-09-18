using System;
using Microsoft.AspNetCore.Mvc;
using VulnerableCoreApp.ViewModels;
using VulnerableCoreApp.Models;
using VulnerableCoreApp.Repository;

namespace VulnerableCoreApp.Controllers
{
    public class CrossSiteScriptingController : Controller
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