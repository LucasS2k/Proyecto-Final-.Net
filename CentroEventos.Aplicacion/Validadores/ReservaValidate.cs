using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.Validadores;
public class ReservaValidador(IRepositorioReserva repositorioReserva)
{
    private readonly IRepositorioReserva _repositorioReserva = repositorioReserva;

    public void Validar(Reserva reserva)
    { 
        if (reserva == null)
        {
            throw new OperacionInvalidaException("La reserva no puede ser nula");
        }

        if (reserva.FechaAltaReserva == DateTime.MinValue)
        {
            throw new OperacionInvalidaException("La fecha de alta de la reserva no puede ser nula");
        }

        if (reserva.EventoId <= 0)
        {
            throw new OperacionInvalidaException("El ID del evento debe ser mayor que cero");
        }

        if (reserva.PersonaId <= 0)
        {
            throw new OperacionInvalidaException("El ID de la persona es invalido");
        }

        if (reserva.Estado != EstadoAsistencia.Pendiente && reserva.Estado != EstadoAsistencia.Presente && reserva.Estado != EstadoAsistencia.Ausente)
        {
            throw new OperacionInvalidaException("El estado de la reserva es invalido");
        }
    }
}