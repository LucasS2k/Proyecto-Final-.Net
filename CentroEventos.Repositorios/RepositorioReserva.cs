using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Repositorios
{
    public class RepositorioReserva : IRepositorioReserva
    {
        private IRepositorioPersona? _repositorioPersona;
        private IRepositorioEventoDeportivo? _repositorioEvento;
        private readonly string _nameFile = "reservas.txt";

        public void SetDependencias(IRepositorioPersona repoPersona, IRepositorioEventoDeportivo repoEvento)
        {
            _repositorioPersona = repoPersona;
            _repositorioEvento = repoEvento;
        }

        private int GenerarID()
        {
            return Listar().Count + 1;
        }
        public void Agregar(Reserva reserva)
        {
            var reservas = Listar();
            int reservasExistentes = reservas.Count(r => r.EventoId == reserva.EventoId);

            var repoEvento = _repositorioEvento;
            var evento = repoEvento.ObtenerEventoPorId(reserva.EventoId);
            //Chequeo si el evento tiene cupo
            //Podria implementarse restandole cupos al archivo
            if (reservasExistentes >= evento.Cupo)
                throw new ValidacionException("El evento esta lleno");

            // Chequeo si la persona ya se registro a este evento
            if (reservas.Any(r => r.PersonaId == reserva.PersonaId && r.EventoId == reserva.EventoId))
                throw new ValidacionException("La persona ya se registro a este evento");

            reserva.Id = GenerarID();
            using var sw = new StreamWriter(_nameFile, true);
            sw.WriteLine(reserva.Id);
            sw.WriteLine(reserva.EventoId);
            sw.WriteLine(reserva.PersonaId);
            sw.WriteLine(reserva.FechaAltaReserva);
        }

        public void Eliminar(int id)
        {
            var reservas = Listar();
            foreach (Reserva r in reservas)
            {
                if (r.Id == id)
                {
                    reservas.Remove(r);
                    break;
                }
            }
            using var sw = new StreamWriter(_nameFile, false);
            foreach (Reserva r in reservas)
            {
                sw.WriteLine(r.Id);
                sw.WriteLine(r.EventoId);
                sw.WriteLine(r.PersonaId);
                sw.WriteLine(r.FechaAltaReserva);
            }
        }
        public List<Reserva> Listar()
        {
            var reservas = new List<Reserva>();
            if (File.Exists(_nameFile))
            {
                using var sr = new StreamReader(_nameFile);
                while (!sr.EndOfStream)
                {
                    var reserva = new Reserva
                    {
                        Id = int.Parse(sr.ReadLine() ?? ""),
                        EventoId = int.Parse(sr.ReadLine() ?? ""),
                        PersonaId = int.Parse(sr.ReadLine() ?? ""),
                        FechaAltaReserva = DateTime.Parse(sr.ReadLine()!),//origen del problema REVISAR
                    };
                    reservas.Add(reserva);
                }
            }
            return reservas;
        }
        public Reserva ObtenerReservaPorId(int id)
        {
            var reservas = Listar();
            foreach (var reserva in reservas)
            {
                if (reserva.Id == id)
                {
                    return reserva;
                }
            }
            throw new EntidadNotFoundException("Reserva no encontrada");
        }
        public void Modificar(Reserva reserva)
        {
            var reservas = Listar();

            for (int i = 0; i < reservas.Count; i++)
            {
                if (reservas[i].Id == reserva.Id)
                {
                    reservas[i] = reserva;
                    break;
                }
            }
            using var sw = new StreamWriter(_nameFile, false);
            foreach (var r in reservas)
            {
                sw.WriteLine(r.Id);
                sw.WriteLine(r.EventoId);
                sw.WriteLine(r.PersonaId);
                sw.WriteLine(r.FechaAltaReserva);
            }
        }
        public List<Persona> ListarAsistenciaAEvento(int eventoId)
        {
            var personas = new List<Persona>();

            if (File.Exists(_nameFile))
            {
                using var sr = new StreamReader(_nameFile);
                while (!sr.EndOfStream)
                {
                    int reservaId = int.Parse(sr.ReadLine() ?? "0");            // línea 1
                    int eventoIdArchivo = int.Parse(sr.ReadLine() ?? "0");     // línea 2
                    int personaId = int.Parse(sr.ReadLine() ?? "0");           // línea 3
                    string fecha = sr.ReadLine();                               // línea 4 (no usada aquí)

                    if (eventoIdArchivo == eventoId)
                    {
                        var persona = _repositorioPersona.ObtenerPersonaPorId(personaId);
                        personas.Add(persona);
                    }
                }
            }

            return personas;
        }
    }
}
