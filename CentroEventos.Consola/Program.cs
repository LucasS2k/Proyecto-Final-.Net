using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Repositorios;
using CentroEventos.Aplicacion.Excepciones;
class Program
{
    static void Main(string[] args)
    {
        var repositorioPersona = new RepositorioPersona();
        var repositorioEvento = new RepositorioEventoDeportivo();
        var repositorioReserva = new RepositorioReserva();
        repositorioPersona.SetDependencias(repositorioEvento, repositorioReserva);
        repositorioEvento.SetDependencias(repositorioReserva, repositorioPersona);
        repositorioReserva.SetDependencias(repositorioPersona, repositorioEvento);
        bool seguir = true;
        while (seguir)
        {
            Console.WriteLine("______________________________________");
            Console.WriteLine("|⚽⚾🏀🏐CENTRO DEPORTIVO🥊🥋🥅⛳ |");
            Console.WriteLine("|____________________________________|");
            Console.WriteLine("| 1 Menu de Personas                 |");
            Console.WriteLine("| 2 Menu de Eventos                  |");
            Console.WriteLine("| 3 Menu de Reservas                 |");
            Console.WriteLine("| 0 Salir                            |");
            Console.WriteLine("|____________________________________|");
            Console.WriteLine("Seleccione una opcion: ");
            string? opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    MenuPersonas();
                    break;
                case "2":
                    MenuEventos();
                    break;
                case "3":
                    MenuReservas();
                    break;
                case "0":
                    seguir = false;
                    break;
                default:
                    MostrarMensaje("Opcion invalida");
                    break;
            }
        }
         void MenuPersonas()
        {
            bool volver = false;
            while (!volver)
            {   //Personas LISTO
                Console.WriteLine("_______________________________");
                Console.WriteLine("|         PERSONAS            |");
                Console.WriteLine("|_____________________________|");
                Console.WriteLine("| 1 Crear/Alta persona        |");
                Console.WriteLine("| 2 Listar personas           |");
                Console.WriteLine("| 3 Modificar persona         |");
                Console.WriteLine("| 4 Eliminar/Baja persona     |");
                Console.WriteLine("| 5 Buscar por DNI            |");
                Console.WriteLine("| 6 Buscar por Email          |");
                Console.WriteLine("| 0 Volver al inicio          |");
                Console.WriteLine("|_____________________________|");
                Console.Write("Seleccione una opcion");
                string? opcion = Console.ReadLine();
                try
                {
                    switch (opcion)
                    {
                        case "1":
                            CrearPersona();
                            break;
                        case "2":
                            ListarPersonas();
                            break;
                        case "3":
                            ModificarPersona();
                            break;
                        case "4":
                            EliminarPersona();
                            break;
                        case "5":
                            BuscarPorDni();
                            break;
                        case "6":
                            BuscarPorEmail();
                            break;
                        case "0":
                            volver = true;
                            break;
                        default:
                            MostrarMensaje("Opcion no valida, ingrese un numero del 1 al 6 (0 para salir)");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error: " + ex.Message);
                }
            }
        }
        //LISTO
         void CrearPersona()
        {
            Console.Write("DNI:");
            string? dni = Console.ReadLine();
            Console.Write("Nombre:");
            string? nombre = Console.ReadLine();
            Console.Write("Apellido:");
            string? apellido = Console.ReadLine();
            Console.Write("Email:");
            string? email = Console.ReadLine();
            Console.Write("Telefono:");
            string? telefono = Console.ReadLine();

            var persona = new Persona
            {
                DNI = dni,
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono
            };
            repositorioPersona.Agregar(persona);
            MostrarMensaje("Persona agregada");
        }
        //LISTO
         void ListarPersonas()
        {
            var personas = repositorioPersona.Listar();
            if (personas.Count == 0)
            {
                MostrarMensaje("No hay personas");
                return;
            }
            Console.WriteLine("Personas:");
            foreach (var p in personas)
            {
                Console.WriteLine(p);
            }
            Pausar();
        }
        //LISTO
         void ModificarPersona()
{
    Console.Write("Ingrese el ID de la persona a modificar: ");
    string? idInput = Console.ReadLine();

    if (!int.TryParse(idInput, out int id))
    {
        MostrarMensaje("El ID debe ser un numero");
        return;
    }
    Persona? persona;
    try
    {
        persona = repositorioPersona.ObtenerPersonaPorId(id);
    }
    catch (EntidadNotFoundException)
    {
        MostrarMensaje("Esa persona no esta registrada");
        return;
    }
    Console.WriteLine("El ID se conserva");
    Console.WriteLine($"Actualizar datos de {persona.Nombre} {persona.Apellido}");

    Console.WriteLine("Nuevo DNI: ");
    string? dni = Console.ReadLine();

    Console.WriteLine("Nuevo Nombre: ");
    string? nombre = Console.ReadLine();

    Console.WriteLine("Nuevo Apellido:");
    string? apellido = Console.ReadLine();

    Console.WriteLine("Nuevo Email: ");
    string? email = Console.ReadLine();

    Console.WriteLine("Nuevo Telefono: ");
    string? telefono = Console.ReadLine();

    persona.DNI = string.IsNullOrWhiteSpace(dni) ? persona.DNI : dni;
    persona.Nombre = string.IsNullOrWhiteSpace(nombre) ? persona.Nombre : nombre;
    persona.Apellido = string.IsNullOrWhiteSpace(apellido) ? persona.Apellido : apellido;
    persona.Email = string.IsNullOrWhiteSpace(email) ? persona.Email : email;
    persona.Telefono = string.IsNullOrWhiteSpace(telefono) ? persona.Telefono : telefono;

    repositorioPersona.Modificar(persona);
    MostrarMensaje("Persona actualizada con éxito.");
}
        //LISTO
         void EliminarPersona()
        {
            Console.WriteLine("Ingrese el ID de la persona a eliminar");
            string? entrada = Console.ReadLine();
            if (!int.TryParse(entrada, out int id))
            {
                MostrarMensaje("ID debe ser un numero");
                return;
            }
            try
            {
                repositorioPersona.Eliminar(id);
                MostrarMensaje("Persona eliminada");
            }
            catch (Exception ex)
                {
                    MostrarMensaje("Error: " + ex.Message);
                }
        }
        //LISTO
         void BuscarPorDni()
        {
            Console.Write("Ingrese el DNI a buscar: ");
            string? dni = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(dni))
            {
                MostrarMensaje("Debe ingresar un DNI");
                return;
            }
            try
            {
                var persona = repositorioPersona.ObtenerPorDni(dni);
                Console.WriteLine(persona);
            }
            catch (EntidadNotFoundException)
            {
                MostrarMensaje("No se encontro a la persona con ese DNI");
            }

            Pausar();
        }
        //LISTO
         void BuscarPorEmail()
        {
            Console.Write("Ingrese el Email a buscar: ");
            string? email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                MostrarMensaje("Debe ingresar un email valido");
                return;
            }
            try
            {
                var persona = repositorioPersona.ObtenerPorEmail(email);
                Console.WriteLine(persona);
            }
            catch (EntidadNotFoundException)
            {
                MostrarMensaje("No se encontro a la persona con ese email");
            }
            Pausar();
        }
         void Pausar()
        {
            Console.WriteLine("Presione una tecla para continuar");
            //Con ReadKey da error REVISAR
            Console.ReadLine();
        }
         void MenuEventos()
        {
            bool volver = false;
            while (!volver)
            {
                Console.WriteLine("_______________________________");
                Console.WriteLine("|         EVENTOS             |");
                Console.WriteLine("|_____________________________|");
                Console.WriteLine("| 1 Agregar Evento            |");
                Console.WriteLine("| 2 Modificar Evento          |");
                Console.WriteLine("| 3 Eliminar/Baja Evento      |");
                Console.WriteLine("| 4 Buscar por ID             |");
                Console.WriteLine("| 5 Buscar por Nombre         |");
                Console.WriteLine("| 6 Listar Eventos            |");
                Console.WriteLine("| 7 Listar Eventos con cupo   |");
                Console.WriteLine("| 0 Volver al inicio          |");
                Console.WriteLine("|_____________________________|");
                Console.Write("Seleccione una opcion");
                string? opcion = Console.ReadLine();
                try
                {
                    switch (opcion)
                    {
                        case "1":
                            AgregarEvento();
                            break;
                        case "2":
                            ModificarEvento();
                            break;
                        case "3":
                            BajaEvento();
                            break;
                        case "4":
                            BuscarPorId();
                            break;
                        case "5":
                            BuscarPorNombre();
                            break;
                        case "6":
                            ListarEventos();
                            break;
                        case "7":
                            ListarEventosConCupoDisponible();
                            break;
                        //case "8":
                           // ListarAsistenciaAEvento();
                          //  break;
                        case "0":
                            volver = true;
                            break;
                        default:
                            MostrarMensaje("Opcion no valida, ingrese un numero del 1 al 7(0 para salir)");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error: " + ex.Message);
                }
            }
        }
        //LISTO
         void AgregarEvento()
{
    Console.Write("Nombre del evento: ");
    string? nombre = Console.ReadLine();//evitamos null
    if (string.IsNullOrWhiteSpace(nombre))
    {
        MostrarMensaje("El nombre no puede estar vacio.");
        return;
    }
    Console.Write("Descripción: ");
    string? descripcion = Console.ReadLine();

    Console.Write("Hora inicio (formato: AAAA-MM-AA 14:00) ");
    string? fechaStr = Console.ReadLine();
    if (!DateTime.TryParse(fechaStr, out DateTime fechaHoraInicio))
    {
        MostrarMensaje("fecha invalida");
        return;
    }
    if (fechaHoraInicio < DateTime.Now)
    {
        MostrarMensaje("La fecha ingresada es previa a la actual");
        return;
    }
    Console.Write("Duracion en horas: ");
    string? duracionStr = Console.ReadLine();
    if (!double.TryParse(duracionStr, out double duracion) || duracion <= 0) //Evitamos duracion invalida
    {
        MostrarMensaje("La duracion debe ser  mayor que 0");
        return;
    }
    Console.Write("Cupo máximo: ");
    string? cupoInput = Console.ReadLine();
    if (!int.TryParse(cupoInput, out int cupo) || cupo <= 0) //evitamos cupos invalidos
    {
        MostrarMensaje("El cupo debe ser mayor que 0");
        return;
    }

    Console.Write("ID del responsable: ");
    string? idResponsableStr = Console.ReadLine();
    if (!int.TryParse(idResponsableStr, out int responsableId) || responsableId <= 0)//Evitamos nulos
    {
        MostrarMensaje("El ID del responsable debe ser un numero");
        return;
    }
    // Verificamos si la persona existe
    try
    {
        var persona = repositorioPersona.ObtenerPersonaPorId(responsableId);
    }
    catch (Exception)
    {
        MostrarMensaje("No se encontro a esa persona");
        return;
    }
     var evento = new EventoDeportivo
    {
        Nombre = nombre,
        Descripcion = descripcion,
        FechaHoraInicio = fechaHoraInicio,
        Duracion = duracion,
        Cupo = cupo,
        ResponsableId = responsableId
    };
            repositorioEvento.Agregar(evento);
            MostrarMensaje("Evento agregado");
        }
         void ModificarEvento()//LISTO
        {
            Console.Write("Ingrese el ID del evento a modificar: ");
    string? idStr = Console.ReadLine();

    if (!int.TryParse(idStr, out int id))
    {
        MostrarMensaje("ID inválido");
        return;
    }

    EventoDeportivo evento;
    try
    {
        evento = repositorioEvento.ObtenerEventoPorId(id);
    }
    catch (Exception)
    {
        MostrarMensaje("Evento inexistente");
        return;
    }
    Console.WriteLine($"Actualizando evento: {evento.Nombre}");
    Console.Write($"Nombre actual ({evento.Nombre}): ");
    string? nuevoNombre = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(nuevoNombre))
        evento.Nombre = nuevoNombre;
    Console.Write($"Descripcion actual ({evento.Descripcion}): ");
    string? nuevaDescripcion = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(nuevaDescripcion))
        evento.Descripcion = nuevaDescripcion;
    Console.Write($"Fecha y hora actual ({evento.FechaHoraInicio}): ");
    string? nuevaFechaStr = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(nuevaFechaStr))
    {
        if (!DateTime.TryParse(nuevaFechaStr, out DateTime nuevaFecha))
        {
            MostrarMensaje("Fecha invalida");
            return;
        }
        if (nuevaFecha < DateTime.Now)
        {
            MostrarMensaje("La nueva fecha no puede ser anterior a la actual");
            return;
        }
        evento.FechaHoraInicio = nuevaFecha;
    }
    Console.Write($"Duración actual ({evento.Duracion} horas): ");
    string? nuevaDuracionStr = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(nuevaDuracionStr))
    {
        if (!double.TryParse(nuevaDuracionStr, out double nuevaDuracion) || nuevaDuracion <= 0)
        {
            MostrarMensaje("Duracion invalida");
            return;
        }
        evento.Duracion = nuevaDuracion;
    }
    Console.Write($"Cupo actual ({evento.Cupo}): ");
    string? nuevoCupoStr = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(nuevoCupoStr))
    {
        if (!int.TryParse(nuevoCupoStr, out int nuevoCupo) || nuevoCupo <= 0)
        {
            MostrarMensaje("Cupo invalido");
            return;
        }
        evento.Cupo = nuevoCupo;
    }
    Console.Write($"ID del responsable actual ({evento.ResponsableId}): ");
    string? nuevoResponsableStr = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(nuevoResponsableStr))
    {
        if (!int.TryParse(nuevoResponsableStr, out int nuevoResponsableId) || nuevoResponsableId <= 0)
        {
            MostrarMensaje("ID de responsable invalido");
            return;
        }
        try
        {
            var responsable = repositorioPersona.ObtenerPersonaPorId(nuevoResponsableId);
            evento.ResponsableId = nuevoResponsableId;
        }
        catch
        {
            MostrarMensaje("No hay una persona con ese ID");
            return;
        }
    }
    try
    {
        repositorioEvento.Modificar(evento);
        MostrarMensaje("Evento modificado");
    }
    catch (Exception ex)
    {
        MostrarMensaje("Error al modificar el evento: " + ex.Message);
    }
        }
         void BajaEvento()
        {
            Console.WriteLine("Ingrese el ID del evento a eliminar");
            string? entrada = Console.ReadLine();
            if (!int.TryParse(entrada, out int id))
            {
                MostrarMensaje("ID debe ser un numero");
                return;
            }
            try
            {
                repositorioEvento.Eliminar(id);
                MostrarMensaje("Evento eliminado");
            }
            catch (Exception)
            {
                MostrarMensaje("Error");
            }
        }
         void BuscarPorId()//LISTO
        {
            Console.WriteLine("Ingrese el ID a buscar");
            string? entrada = Console.ReadLine();
            // var evento = eventosRepo.ObtenerEventoPorId(int.Parse(id));
            //Console.WriteLine(evento.ToString());
            if (!int.TryParse(entrada, out int id))
            {
                MostrarMensaje("Debe ingresar un numero");
                return;
            }

            try
            {
                var evento = repositorioEvento.ObtenerEventoPorId(id);
                Console.WriteLine(evento.ToString());
            }
            catch (Exception ex)
                {
                    MostrarMensaje("Error: " + ex.Message);
                }
            Pausar();
        }
         void BuscarPorNombre()//LISTO
        {
            Console.WriteLine("Ingrese el nombre a buscar:");
            string? nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarMensaje("Debe ingresar un string valido");
                return;
            }
            try
            {
                var evento = repositorioEvento.ObtenerEventoPorNombre(nombre);
                Console.WriteLine(evento);
            }
            catch (Exception)
            {
                MostrarMensaje($"Evento {nombre} no encontrado");
            }
            Pausar();
        }
       void ListarEventos()//LISTO
        {
            var eventos = repositorioEvento.Listar();
            if (eventos.Count == 0)
            {
                MostrarMensaje("No hay eventos registrados");
                return;
            }
            foreach (var e in eventos)
            {
                Console.WriteLine(e);
            }
            Pausar();
        }
         void ListarEventosConCupoDisponible()
{
    var eventosConCupo = repositorioEvento.ListarEventosConCupoDisponible();
    if (eventosConCupo.Count == 0)
    {
        MostrarMensaje("No hay eventos con cupo disponible");
        return;
    }
    Console.WriteLine("Eventos con cupo:");
    foreach (var evento in eventosConCupo)
    {
                Console.WriteLine(evento);
    }
    Pausar();
}       
        //LISTO
         void MenuReservas()
        {
            bool volver = false;
            while (!volver)
            {
                Console.WriteLine("_______________________________");
                Console.WriteLine("|         RESERVAS            |");
                Console.WriteLine("|_____________________________|");
                Console.WriteLine("| 1 Agregar Reserva           |");
                Console.WriteLine("| 2 Modificar Reserva         |");
                Console.WriteLine("| 3 Eliminar/Baja Reserva     |");
                Console.WriteLine("| 4 Buscar por ID             |");
                Console.WriteLine("| 5 Listar Reservas           |");
                Console.WriteLine("| 6 Listar Asistencia a Evento|");
                Console.WriteLine("| 0 Volver al inicio          |");
                Console.WriteLine("|_____________________________|");
                Console.Write("Seleccione una opcion");
                string? opcion = Console.ReadLine();
                try
                {
                    switch (opcion)
                    {
                        case "1":
                            AgregarReserva();
                            break;
                        case "2":
                            ModificarReserva();
                            break;
                        case "3":
                            BajaReserva();
                            break;
                        case "4":
                            BuscarReservaPorId();
                            break;
                        case "5":
                            ListarReservas();
                            break;
                        case "6":
                            ListarAsistenciaAEvento();
                            break;
                        case "0":
                            volver = true;
                            break;
                        default:
                            MostrarMensaje("Opcion no valida, ingrese un numero del 1 al 6");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error: " + ex.Message);
                }
            }
        }
        void AgregarReserva()
        {
            Console.Write("Ingrese el ID del evento: ");
            string? eventoStr = Console.ReadLine();
            Console.Write("Ingrese el ID de la persona: ");
            string? personaStr = Console.ReadLine();
            if (!int.TryParse(eventoStr, out int eventoId) || !int.TryParse(personaStr, out int personaId))
            {
                MostrarMensaje("Los IDs no son validos");
                return;
            }
            try
            {  //se verifica que existan los ids ingresados
                var evento = repositorioEvento.ObtenerEventoPorId(eventoId);
                var persona = repositorioPersona.ObtenerPersonaPorId(personaId);
                var reserva = new Reserva
                {
                    EventoId = eventoId,
                    PersonaId = personaId,
                    FechaAltaReserva = DateTime.Now
                };
                repositorioReserva.Agregar(reserva);
                MostrarMensaje("Reserva agregada");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message);
            }
        }
         void ModificarReserva()
        {
            Console.Write("Ingrese el ID de la reserva a modificar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensaje("ID no valido");
                return;
            }
            var reserva = repositorioReserva.ObtenerReservaPorId(id);

            Console.Write("Nuevo ID del evento (actual:" + reserva.EventoId + ")");
            if (int.TryParse(Console.ReadLine(), out int nuevoEventoId))
            {
                reserva.EventoId = nuevoEventoId;
            }
            Console.Write("Nuevo ID de persona (actual:" + reserva.PersonaId + ")");
            if (int.TryParse(Console.ReadLine(), out int nuevaPersonaId))
            {
                reserva.PersonaId = nuevaPersonaId;
            }
            reserva.FechaAltaReserva = DateTime.Now;
            repositorioReserva.Modificar(reserva);
            MostrarMensaje("Reserva modificada correctamente");
            Pausar();
        }
         void BajaReserva()
        {
            Console.Write("Ingrese el ID de la reserva a eliminar:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensaje("ID no valido");
                return;
            }
            repositorioReserva.Eliminar(id);
            MostrarMensaje("Reserva eliminada correctamente.");
            Pausar();
        }
         void BuscarReservaPorId()
        {
            Console.Write("Ingrese el ID de la reserva:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensaje("ID no valido");
                return;
            }
            var reserva = repositorioReserva.ObtenerReservaPorId(id);
            Console.WriteLine(reserva.ToString());
            Pausar();
        }
         void ListarReservas()
        {
            var reservas = repositorioReserva.Listar();
            if (reservas.Count == 0)
            {
                MostrarMensaje("No hay reservas registradas");
                return;
            }
            Console.WriteLine("Lista de reservas:");
            foreach (var reserva in reservas)
            {
                Console.WriteLine(reserva.ToString());
            }
            Pausar();
        }
        //LISTO
        void ListarAsistenciaAEvento()
        {
            Console.Write("Ingrese el ID del evento para listar asistencia:");
            string? entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out int id))
            {
                MostrarMensaje("ID invalido");
                return;
            }
            var evento = repositorioEvento.ObtenerEventoPorId(id);
            var personas = repositorioReserva.ListarAsistenciaAEvento(evento.Id);
            if (personas.Count == 0)
            {
                MostrarMensaje($"No hay personas registradas para el evento: {evento.Nombre}");
                return;
            }
            Console.WriteLine($"Personas registradas para el evento: {evento.Nombre}");
            foreach (var persona in personas)
            {
                Console.WriteLine(persona);
            }
            Pausar();
        }
        static void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
            Console.WriteLine("Presione una tecla para continuar");
        }
    Console.WriteLine("------Fin programa-----");
Console.WriteLine("Presione una tecla para salir");
Console.ReadLine();
    }
}