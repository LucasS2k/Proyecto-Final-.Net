using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Repositorios
{
    public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
{
    private IRepositorioReserva? _repositorioReserva;
    private IRepositorioPersona? _repositorioPersona;
    private readonly string _nameFile = "eventos.txt";

    public void SetDependencias(IRepositorioReserva repoReserva, IRepositorioPersona repoPersona)
    {
        _repositorioReserva = repoReserva;
        _repositorioPersona = repoPersona;
    }

        private int GenerarID()
        {//el id puede ser uno que ya existio y fue eliminado //REVISAR//
            return Listar().Count + 1;
        }
        public void Agregar(EventoDeportivo evento)
        {if (evento.FechaHoraInicio < DateTime.Now)
                throw new ValidacionException("Fecha invalida");
        evento.Id = GenerarID();
        
            using var sw = new StreamWriter(_nameFile, true);
            
            sw.WriteLine(evento.Id);
            sw.WriteLine(evento.Nombre);
            sw.WriteLine(evento.Descripcion);
            sw.WriteLine(evento.FechaHoraInicio);
            sw.WriteLine(evento.Duracion);
            sw.WriteLine(evento.Cupo);
            sw.WriteLine(evento.ResponsableId);
        }
        public List<EventoDeportivo> Listar()
        {
            var eventos = new List<EventoDeportivo>();
            if (!File.Exists(_nameFile))
                return eventos;

            using var sr = new StreamReader(_nameFile);
            while (!sr.EndOfStream)
            {
                int id = int.Parse(sr.ReadLine()!);
                string nombre = sr.ReadLine()!;
                string descripcion = sr.ReadLine()!;
                DateTime fecha = DateTime.Parse(sr.ReadLine()!);
                double duracion = double.Parse(sr.ReadLine()!);
                int cupo = int.Parse(sr.ReadLine()!);
                int responsableId = int.Parse(sr.ReadLine()!);

                eventos.Add(new EventoDeportivo
                {
                    Id = id,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    FechaHoraInicio = fecha,
                    Duracion = duracion,
                    Cupo = cupo,
                    ResponsableId = responsableId
                });
            }

            return eventos;
        }
        public EventoDeportivo ObtenerEventoPorId(int id)
        {
            var eventos = Listar();
            foreach (var evento in eventos)
            {
                if (evento.Id == id)
                {
                    return evento;
                }
            }
            throw new EntidadNotFoundException("Evento no encontrado");
        }
        public void Modificar(EventoDeportivo evento)
        {
            var eventos = Listar();
             var existente = eventos.FirstOrDefault(e => e.Id == evento.Id);
    if (existente == null)
        throw new EntidadNotFoundException("Evento no encontrado");

    if (existente.FechaHoraInicio < DateTime.Now)
        throw new ValidacionException("No se puede modificar un evento finalizado");

    if (evento.FechaHoraInicio < DateTime.Now)
        throw new ValidacionException("La fecha es previa a la actual");
            for (int i = 0; i < eventos.Count; i++)
            {
                if (eventos[i].Id == evento.Id)
                {
                    eventos[i] = evento;
                    break;
                }
            }
            using var sw = new StreamWriter(_nameFile, false);
            foreach (var e in eventos)
            {
                sw.WriteLine(e.Id);
                sw.WriteLine(e.Nombre);
                sw.WriteLine(e.Descripcion);
                sw.WriteLine(e.FechaHoraInicio);
                sw.WriteLine(e.Duracion);
                sw.WriteLine(e.Cupo);
                sw.WriteLine(e.ResponsableId);
            }
        }
        //Se puede manejar de otra manera para no borrarlo por completo y solo cambiar el estado del evento
        public void Eliminar(int id)
    {
        var reservas = _repositorioReserva.Listar();
        if (reservas.Any(r => r.EventoId == id))
            throw new ValidacionException("El evento tiene reservas, no puede eliminarse");

        var eventos = Listar();
        eventos.RemoveAll(e => e.Id == id);

        using var sw = new StreamWriter(_nameFile, false);
        foreach (var evento in eventos)
        {
            sw.WriteLine(evento.Id);
            sw.WriteLine(evento.Nombre);
            sw.WriteLine(evento.Descripcion);
            sw.WriteLine(evento.FechaHoraInicio);
            sw.WriteLine(evento.Duracion);
            sw.WriteLine(evento.Cupo);
            sw.WriteLine(evento.ResponsableId);
        }
    }
        public List<EventoDeportivo> ListarEventosConCupoDisponible()
        {
            var eventos = Listar();
            return eventos.Where(e => e.Cupo > 0).ToList();
        }

        //public List<EventoDeportivo> ListarAsistenciaAEvento(int id)
        //{
         //   var eventos = Listar();
          //  return eventos.Where(e => e.Id == id).ToList();
        //}
        public EventoDeportivo ObtenerEventoPorNombre(string nombre)
        {
            var eventos = Listar();
            foreach (var evento in eventos)
            {
                if (evento.Nombre?.Equals(nombre, StringComparison.OrdinalIgnoreCase)==true) //para evitar nulos
                {
                    return evento;
                }
            }
            throw new EntidadNotFoundException("Evento no encontrado");
        }
    } 
    }

