
namespace Solution
{
    using Bussines;
    using Common;
    using Enums;
    using Models;

    public class Program
    {

        public static void Main(string[] args)
        {
            Run();
        }
        public static void Run()
        {
            IBarracoBussines barracoBussines = IoC.getBarracoBussines();
            Corno corno = new Corno();
            List<Corno> cornos = new List<Corno>();
            FilhoDaPuta ricardão = new FilhoDaPuta();

            try
            {
                corno.Trampo = Trampo.CantorDeForro;
                string desfecho = barracoBussines.EsfaquearORicardao(corno, ricardão);
                Console.WriteLine(desfecho);
                cornos.Add(new Corno() { Id = 1,  Trampo = Trampo.CantorDeForro }) ;
                cornos.Add(new Corno() { Id = 2, Trampo = Trampo.Advogado });
                cornos.Add(new Corno() { Id = 3, Trampo = Trampo.CantorDeForro });
                Dictionary<int, bool> cornosQuePodemEsfaquar = barracoBussines.CornosQuePodemEsfaquar(cornos, ricardão);
                foreach (var item in cornosQuePodemEsfaquar)
                {
                    Console.WriteLine($"Corno {item.Key} pode esfaquear o Ricardão? {item.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}


namespace Common
{
    using Application;
    using Bussines;
    public static class IoC
    {
        public static IBarracoBussines getBarracoBussines()
        {
            IBarracoBussines barracoBussines = new BarracoBussines();
            return barracoBussines;
        }
    }
}

namespace Application
{
    using Bussines;
    using Models;
    using System.Collections.Generic;

    public class BarracoBussines : IBarracoBussines
    {
        public Dictionary<int, bool> CornosQuePodemEsfaquar(List<Corno> cornos, FilhoDaPuta ricardão)
        {

            IEnumerable<Corno> cornosQuePodemEsfaquar =
                from corno in cornos
                where corno.Id != 0 && Corno.PodeEsfaquar().Invoke(corno)
                select corno;
            return cornosQuePodemEsfaquar.ToDictionary(corno => corno.Id, corno => true); ;
        }

        public string EsfaquearORicardao(Corno corno, FilhoDaPuta ricardão)
        {
            if(Corno.PodeEsfaquar().Invoke(corno))
            {
                return "Esfaqueou o Ricardão";
            }
            return "não deu pra esfaquear o Ricardão";
        }
    }
}

namespace Bussines
{
    using Models;
    public interface IBarracoBussines
    {
        public string EsfaquearORicardao(Corno corno, FilhoDaPuta ricardão);
        public Dictionary<int, bool> CornosQuePodemEsfaquar(List<Corno> corno, FilhoDaPuta ricardão);

    }
}
namespace Models
{
    using Enums;
    public class Entity
    {
        public int Id { get; set; }
    }
    public class FilhoDaPuta : Entity
    {
        public FilhoDaPuta PutaQuePariu { get; set; }
        public Corno PaiSumido { get; set; }
    }
    public class Corno : Entity
    {
        private static List<Trampo> trampoDeCorno = new List<Trampo>() {
            Trampo.VigilanteNoturno,
            Trampo.Caminhoneiro,
            Trampo.Taxista,
            Trampo.Policial,
            Trampo.MotoristaDeOnibus,
            Trampo.CantorDeForro
        };
        public Trampo Trampo { get; set; }
        public FilhoDaPuta Dona { get; set; }
        public List<FilhoDaPuta> Ricardões { get; set; }
        public static Func<Corno, bool> PodeEsfaquar()
        {
            return corno => trampoDeCorno.Contains(corno.Trampo);
        }
    }

}
namespace Enums
{
    public enum Trampo
    {
        Engenheiro,
        Medico,
        Advogado,
        Cientista,
        Professor,
        Empresario,
        VigilanteNoturno,
        Caminhoneiro,
        Taxista,
        Policial,
        MotoristaDeOnibus,
        CantorDeForro
    }
}
