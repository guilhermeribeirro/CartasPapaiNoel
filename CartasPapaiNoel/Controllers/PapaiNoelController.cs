using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CartasPapaiNoel.Models.Request;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Json;

namespace CartasPapaiNoel.Controllers


{
    [Route("api/[controller]")]

    [ApiController]


    public class PapaiNoelController: PrincipalController
    {
        private readonly string _produtoCaminhoArquivo;


        public PapaiNoelController()


        {
            _produtoCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "PapaiNoel.json");


        }


        #region  Metodos do Arquivo
        private List<PapaiNoelViewModel> LerCartas()
        {
            if (!System.IO.File.Exists(_produtoCaminhoArquivo))
            {
                return new List<PapaiNoelViewModel>();


            }

            string json = System.IO.File.ReadAllText(_produtoCaminhoArquivo);

            return JsonConvert.DeserializeObject<List<PapaiNoelViewModel>>(json);

        }


        private int ObterProximoCodigoDisponivel()
        {
            List<PapaiNoelViewModel> cartas = LerCartas();

            if (cartas.Any())
            {

                return cartas.Max(p => p.CodigoCarta) + 1;

            }
            else
            {

                return 1;
            }

        }

        private void EscreverCartasNoArquivo(List<PapaiNoelViewModel> cartas)


        {
            string json = JsonConvert.SerializeObject(cartas);

            System.IO.File.WriteAllText(_produtoCaminhoArquivo, json);


        }





        #endregion



        #region   Operações do CRUD

        [HttpGet]

        public IActionResult Get()
        {
            List<PapaiNoelViewModel> cartas = LerCartas();
            return Ok(cartas);
        }





        [HttpPost]

        public IActionResult Post([FromBody] NovoPapaiNoelViewModel carta)
        {
            if (!ModelState.IsValid)



                return ApiBadRequestResponse(ModelState);

            List<PapaiNoelViewModel> cartas = LerCartas();
            int proximoCodigo = ObterProximoCodigoDisponivel();

            PapaiNoelViewModel novocarta = new PapaiNoelViewModel()
            {
                CodigoCarta = proximoCodigo,

                Nome = carta.Nome,
                EnderecoCompleto = carta.EnderecoCompleto,
                Idade = carta.Idade,
                TextoCarta = carta.TextoCarta,










            };

            cartas.Add(novocarta);
            EscreverCartasNoArquivo(cartas);

            return ApiResponse(novocarta, "Carta registada com sucesso!");






        }



        #endregion



    }











}