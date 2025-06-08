using System;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoCDU
{
    public class ListarAsistenciaAEventoUseCase
    {
        private readonly IRepositorioEventoDeportivo _repositorioEvento;
        private readonly IRepositorioPersona _repositorioPersona;
        private readonly IServicioAutorizacion _servicioAutorizacion;

        public ListarAsistenciaAEventoUseCase(
            IRepositorioEventoDeportivo repositorioEvento,
            IRepositorioPersona repositorioPersona,
            IServicioAutorizacion servicioAutorizacion)
        {
            _repositorioEvento = repositorioEvento;
            _repositorioPersona = repositorioPersona;
            _servicioAutorizacion = servicioAutorizacion;
        }

        public List<Persona> Ejecutar(int idUsuario, int idEvento)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.EventoModificacion))
            {
                throw new FalloAutorizacionException("No tiene permiso para listar asistencia a eventos.");
            }

            var evento = _repositorioEvento.ObtenerEventoPorId(idEvento);
            if (evento == null)
            {
                throw new EntidadNotFoundException("El evento no existe.");
            }

            return _repositorioPersona.Listar();
        }
    }
}