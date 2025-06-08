using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoCDU
{
    public class EventoAltaUseCase
    {
        private readonly IRepositorioEventoDeportivo _repositorioEvento;
        private readonly IServicioAutorizacion _servicioAutorizacion;

        public EventoAltaUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion)
        {
            _repositorioEvento = repositorioEvento;
            _servicioAutorizacion = servicioAutorizacion;
        }

        public void Ejecutar(EventoDeportivo evento, int idUsuario)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.EventoAlta))
            {
                throw new FalloAutorizacionException("No tiene permiso para realizar esto");
            }

            _repositorioEvento.Agregar(evento);
        }
    }
}