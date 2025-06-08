using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Interfaces;
public interface IRepositorioReserva
{
    void Agregar(Reserva reserva);
    void Modificar(Reserva reserva);
    void Eliminar(int id);
    Reserva ObtenerReservaPorId(int id);
    List<Reserva> Listar();
}