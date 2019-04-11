using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebExemplo.Models;

namespace WebExemplo.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoClient mongoDbClient;
        private List<Aluno> alunos;

        public HomeController(MongoDB.Driver.MongoClient mongoDbClient)
        {
            this.mongoDbClient = mongoDbClient;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            
            var collection = mongoDbClient.GetDatabase("admin").GetCollection<Aluno>("aluno");
            collection.InsertMany(new[] {

            new Aluno() { Nome = "Aluno1", Idade = 17 },

             new Aluno() { Nome = "Aluno2", Idade = 18 },

            new Aluno() { Nome = "Aluno3", Idade = 19 }
            });
            return Ok("3 alunosadcionados");
        }

        public IActionResult Privacy()
        {
            

            ViewData["Message"] = "Your application description page.";
            var listaAluno = mongoDbClient.GetDatabase("admin").GetCollection<Aluno>("aluno").Find(Builders<Aluno>.Filter.Where(it => it.Idade > 0)).ToList();
            
            return View(listaAluno);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
