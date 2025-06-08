using System;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoCDU
{
    public class ListarEventosConCupoDisponibleUseCase
    {
        private readonly IRepositorioEventoDeportivo _repositorioEvento;
        private readonly IServicioAutorizacion _servicioAutorizacion;

        public ListarEventosConCupoDisponibleUseCase(
            IRepositorioEventoDeportivo repositorioEvento,
            IServicioAutorizacion servicioAutorizacion)
        {
            _repositorioEvento = repositorioEvento;
            _servicioAutorizacion = servicioAutorizacion;
        }

        public List<EventoDeportivo> Ejecutar(int idUsuario)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.EventoModificacion))
            {
                throw new FalloAutorizacionException("No tiene permiso para listar eventos.");
            }

            return _repositorioEvento.ListarEventosConCupoDisponible();
        }
    }
}