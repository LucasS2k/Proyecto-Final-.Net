using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
namespace CentroEventos.Aplicacion.Interfaces;
public interface IRepositorioEventoDeportivo
{
    void Agregar(EventoDeportivo evento);
    void Modificar(EventoDeportivo evento);
    void Eliminar(int id);
    EventoDeportivo ObtenerEventoPorId(int id);
    EventoDeportivo ObtenerEventoPorNombre(string nombre);
    List<EventoDeportivo> Listar();
    //List<EventoDeportivo> ListarAsistenciaAEvento(int id);
    List<EventoDeportivo> ListarEventosConCupoDisponible();
}