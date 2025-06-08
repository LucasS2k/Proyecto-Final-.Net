using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Entidades;
namespace CentroEventos.Aplicacion.CasosDeUso.EventoCDU
{
    public class EventoModificacionUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion)
    {
        private readonly IRepositorioEventoDeportivo _repositorioEvento = repositorioEvento;
            private readonly IServicioAutorizacion _servicioAutorizacion = servicioAutorizacion;


        public void ModificarEvento(EventoDeportivo evento, int idUsuario)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.EventoModificacion))
            {
                throw new UnauthorizedAccessException("No tiene permiso para realizar esto");
            }

            _repositorioEvento.Modificar(evento);
        }
    }
}