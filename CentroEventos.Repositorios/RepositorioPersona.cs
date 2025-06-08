using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.CasosDeUso;
using System;
namespace CentroEventos.Repositorios
{ 
    public class RepositorioPersona : IRepositorioPersona
{
    private IRepositorioEventoDeportivo? _repositorioEvento;
    private IRepositorioReserva? _repositorioReserva;
    private readonly string _nameFile = "personas.txt";

    public void SetDependencias(IRepositorioEventoDeportivo repoEvento, IRepositorioReserva repoReserva)
    {
        _repositorioEvento = repoEvento;
        _repositorioReserva = repoReserva;
    }
        private int GenerarID()
        {//el id puede ser uno que ya existio y fue eliminado //REVISAR//
            return Listar().Count + 1;
        }
        public void Agregar(Persona persona)
        {
            persona.Id = GenerarID();
            using var sw = new StreamWriter(_nameFile, true);
            sw.WriteLine(persona.Id);
            sw.WriteLine(persona.DNI);
            sw.WriteLine(persona.Nombre);
            sw.WriteLine(persona.Apellido);
            sw.WriteLine(persona.Email);
            sw.WriteLine(persona.Telefono);
        }
        public void Eliminar(int id)
        {//Chequeo que la persona pueda eliminarse //REVISAR
            var reservasRepo = _repositorioReserva;
            var eventosRepo = _repositorioEvento;
            if (reservasRepo.Listar().Any(r => r.PersonaId == id))
                throw new ValidacionException("La persona tiene reservas pendientes");
            if (eventosRepo.Listar().Any(e => e.ResponsableId == id))
            throw new ValidacionException("La persona es responsable de un evento");
            var personas = Listar();
            foreach (Persona p in personas)
            {
                if (p.Id == id)
                {//remove solo elimina la primera coincidencia (pero no puede haber ids repetidos))
                    personas.Remove(p);
                    break;
                }
            }
            {
                using var sw = new StreamWriter(_nameFile, false);
                foreach (Persona p in personas)
                {
                    sw.WriteLine(p.Id);
                    sw.WriteLine(p.DNI);
                    sw.WriteLine(p.Nombre);
                    sw.WriteLine(p.Apellido);
                    sw.WriteLine(p.Email);
                    sw.WriteLine(p.Telefono);
                }
            }
        }

        public List<Persona> Listar()
        {
            var personas = new List<Persona>();
            if (File.Exists(_nameFile))
            {
                using var sr = new StreamReader(_nameFile);
                while (!sr.EndOfStream)
                {
                    var persona = new Persona
                    { //posible null
                        Id = int.Parse(sr.ReadLine() ?? ""),
                        DNI = sr.ReadLine(),
                        Nombre = sr.ReadLine(),
                        Apellido = sr.ReadLine(),
                        Email = sr.ReadLine(),
                        Telefono = sr.ReadLine()
                    };
                    personas.Add(persona);
                }
            }
            return personas;
        }
        public Persona ObtenerPersonaPorId(int id)
        {
            var personas = Listar();
            foreach (var persona in personas)
            {
                if (persona.Id == id)
                {
                    return persona;
                }
            }
            throw new EntidadNotFoundException($"Persona no encontrada");
        }
        private void GuardarTodo(List<Persona> personas)
        {// false sobreescribe el archivo
            using var sw = new StreamWriter(_nameFile, false);

            foreach (var persona in personas)
            {
                sw.WriteLine(persona.Id);
                sw.WriteLine(persona.DNI);
                sw.WriteLine(persona.Nombre);
                sw.WriteLine(persona.Apellido);
                sw.WriteLine(persona.Email);
                sw.WriteLine(persona.Telefono);
            }
        }
        public void Modificar(Persona persona)
        {
            var listaTotal = Listar();

            for (int i = 0; i < listaTotal.Count; i++)
            {
                if (listaTotal[i].Id == persona.Id)
                {
                    listaTotal[i] = persona;
                    break;
                }
            }

            GuardarTodo(listaTotal); // persistir cambios
        }
        public Persona ObtenerPorDni(string dni)
        {
            var personas = Listar();
            foreach (var persona in personas)
            {
                if (persona.DNI == dni)
                {
                    return persona;
                }
            }
            throw new EntidadNotFoundException("Dni no encontrado"); //REVISAR
        }

        public Persona ObtenerPorEmail(string email)
        {
            var personas = Listar();
            foreach (var persona in personas)
            {
                if (persona.Email == email)
                {
                    return persona;
                }
            }
            throw new EntidadNotFoundException("Email no encontrado"); //REVISAR
        }
    }
}