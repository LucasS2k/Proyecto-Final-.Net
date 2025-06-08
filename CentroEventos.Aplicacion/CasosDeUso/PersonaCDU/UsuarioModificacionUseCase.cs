using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaCDU
{

    public class UsuarioModificacionUseCase
    {
        private readonly IRepositorioPersona _repositorioPersona;
        private readonly IServicioAutorizacion _servicioAutorizacion;
        private readonly PersonaValidador _validadorPersona;

        public UsuarioModificacionUseCase(
            IRepositorioPersona repositorioPersona,
            IServicioAutorizacion servicioAutorizacion,
            PersonaValidador validadorPersona)
        {
            _repositorioPersona = repositorioPersona;
            _servicioAutorizacion = servicioAutorizacion;
            _validadorPersona = validadorPersona;
        }

        public void Ejecutar(Persona persona, int idUsuario)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioModificacion))
            {
                throw new FalloAutorizacionException("No tiene permiso para modificar una persona");
            }

            if (persona == null)
            {
                throw new ValidacionException("La persona no puede ser nula");
            }

            _validadorPersona.Validar(persona); // Excepcion en el metodo

            var existente = _repositorioPersona.ObtenerPersonaPorId(idUsuario);
            if (existente == null)
            {
                throw new EntidadNotFoundException("La persona no existe en la base de datos");
            }

            _repositorioPersona.Modificar(persona);
        }
    }
} 