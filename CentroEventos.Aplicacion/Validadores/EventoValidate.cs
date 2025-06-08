using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.Validadores
{
    public class EventoValidador(IRepositorioEventoDeportivo repositorio, IRepositorioPersona repositorioPersona)
    {
    private readonly IRepositorioEventoDeportivo _repositorio = repositorio;
    private readonly IRepositorioPersona _repositorioPersona = repositorioPersona;

        public void Validar(EventoDeportivo evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre) || string.IsNullOrWhiteSpace(evento.Descripcion))
        {
            throw new ValidacionException("El nombre y la descripción no deben estar vacíos.");
        }

        if (evento.FechaHoraInicio < DateTime.Now)
        {
            throw new ValidacionException("La fecha de inicio debe ser futura o actual.");
        }

        if (evento.Cupo <= 0)
        {
            throw new ValidacionException("El cupo máximo debe ser mayor que cero.");
        }

        var responsable = _repositorioPersona.ObtenerPersonaPorId(evento.ResponsableId);
        if (responsable == null)
        {
            throw new EntidadNotFoundException($"No se encontró persona con ID {evento.ResponsableId}.");
        }

        var existentePorNombre = _repositorio.ObtenerEventoPorNombre(evento.Nombre);
        if (existentePorNombre != null && existentePorNombre.Id != evento.Id)
        {
            throw new DuplicadoException($"Ya existe un evento con el nombre {evento.Nombre}.");
        }
    }
}
}