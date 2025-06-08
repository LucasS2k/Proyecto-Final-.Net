using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaCDU
{
    public class UsuarioAltaUseCase
    {
        private readonly IRepositorioPersona _repositorioPersona;
        private readonly IServicioAutorizacion _servicioAutorizacion;
        private readonly PersonaValidador _validador;

        public UsuarioAltaUseCase(
            IRepositorioPersona repositorioPersona,
            IServicioAutorizacion servicioAutorizacion,
            PersonaValidador validador)
        {
            _repositorioPersona = repositorioPersona;
            _servicioAutorizacion = servicioAutorizacion;
            _validador = validador;
        }

        public void Ejecutar(Persona persona, int idUsuario)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioAlta))
            {
                throw new FalloAutorizacionException("No tiene permiso para dar de alta una persona");
            }

            if (persona == null)
            {
                throw new ValidacionException("La persona no puede ser nula.");
            }

            _validador.Validar(persona);

            _repositorioPersona.Agregar(persona);
        }
    }
}