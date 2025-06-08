using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaCDU
{

    public class UsuarioBajaUseCase
    {
        private readonly IRepositorioPersona _repositorioPersona;
        private readonly IServicioAutorizacion _servicioAutorizacion;
        private readonly PersonaValidador _personaValidador;

        public UsuarioBajaUseCase(
            IRepositorioPersona repositorioPersona,
            IServicioAutorizacion servicioAutorizacion,
            PersonaValidador personaValidador)
        {
            _repositorioPersona = repositorioPersona;
            _servicioAutorizacion = servicioAutorizacion;
            _personaValidador = personaValidador;
        }

        public void Ejecutar(Persona persona, int idUsuario)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioBaja))
            {
                throw new FalloAutorizacionException("No tiene permiso para dar de baja una persona");
            }

            if (persona == null)
            {
                throw new ValidacionException("La persona no puede ser nula.");
            }

            _personaValidador.Validar(persona); // Excepcion en el metodo

            var existente = _repositorioPersona.ObtenerPersonaPorId(idUsuario);
            if (existente == null)
            {
                throw new EntidadNotFoundException("La persona no existe en la base de datos");
            }

            _repositorioPersona.Eliminar(idUsuario);
        }
    }
}