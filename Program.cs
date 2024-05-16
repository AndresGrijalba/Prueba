using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;

namespace RegistroPaciente
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Bienvenido al sistema de registro de pacientes.");
                Console.WriteLine("1. Registrar nuevo paciente");
                Console.WriteLine("2. Listar pacientes");
                Console.WriteLine("3. Salir");

                Console.Write("Ingrese el número de la opción que desea: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarPaciente();
                        break;
                    case "2":
                        LeerInformacionPacientes();
                        break;
                    case "3":
                        Console.WriteLine("Gracias por usar el sistema. ¡Hasta luego!");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, ingrese un número válido.");
                        break;
                }

                Console.WriteLine();
            }

        }

        static void RegistrarPaciente()
        {
            Console.WriteLine("");
            Console.WriteLine("Bienvenido al sistema de registro de pacientes.");
            Console.WriteLine("Por favor, ingrese los datos del paciente:");

            Console.Write("Tipo de documento: ");
            string tipoDocumento = Console.ReadLine();

            Console.Write("Número de documento: ");
            string numDocumento = Console.ReadLine();

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine();

            Console.WriteLine("\nPor favor, ingrese los siguientes datos adicionales:");

            Console.Write("Sexo (masculino/femenino): ");
            string sexo = Console.ReadLine();

            Console.Write("Fecha de nacimiento (YYYY-MM-DD): ");
            DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine());

            int edad = CalcularEdad(fechaNacimiento);

            Console.Write("Dirección: ");
            string direccion = Console.ReadLine();

            Console.Write("Correo electrónico: ");
            string correoElectronico = Console.ReadLine();

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();

            Console.Write("Municipio: ");
            string municipio = Console.ReadLine();

            Console.Write("Departamento: ");
            string departamento = Console.ReadLine();

            Console.WriteLine("\nPor favor, ingrese información médica adicional:");

            Console.Write("Enfermedades previas (si las hay): ");
            string enfermedadesPrevias = Console.ReadLine();

            Console.Write("Tratamientos previos (si los hay): ");
            string tratamientosPrevios = Console.ReadLine();

            Console.Write("Impresión diagnóstica: ");
            string impresionDiagnostica = Console.ReadLine();

            string usuario = GenerarUsuario(nombre, apellidos, fechaNacimiento);
            string password = GenerarPassword();

            List<string> medicosResponsables = new List<string>();

            do
            {
                Console.Write("Ingrese el nombre del médico responsable o escriba 'fin' para terminar: ");
                string medico = Console.ReadLine();
                if (medico.ToLower() != "fin")
                {
                    medicosResponsables.Add(medico);
                }
                else
                {
                    break;
                }
            } while (true);

            foreach (string medico in medicosResponsables)
            {
                AsignarPacienteAMedico(medico, nombre, apellidos, tipoDocumento, numDocumento);
            }

            List<string> examenesLaboratorio = new List<string>();

            do
            {
                Console.Write("Ingrese el código del examen de laboratorio realizado o escriba 'fin' para terminar: ");
                string examen = Console.ReadLine();
                if (examen.ToLower() != "fin")
                {
                    examenesLaboratorio.Add(examen);
                }
                else
                {
                    break;
                }
            } while (true);

            Console.WriteLine("\nResumen de la información ingresada:");
            Console.WriteLine($"Tipo de documento: {tipoDocumento}");
            Console.WriteLine($"Número de documento: {numDocumento}");
            Console.WriteLine($"Nombre: {nombre}");
            Console.WriteLine($"Apellidos: {apellidos}");
            Console.WriteLine($"Sexo: {sexo}");
            Console.WriteLine($"Fecha de nacimiento: {fechaNacimiento.ToShortDateString()}");
            Console.WriteLine($"Edad: {edad}");
            Console.WriteLine($"Dirección: {direccion}");
            Console.WriteLine($"Correo electrónico: {correoElectronico}");
            Console.WriteLine($"Teléfono: {telefono}");
            Console.WriteLine($"Municipio: {municipio}");
            Console.WriteLine($"Departamento: {departamento}");
            Console.WriteLine($"Enfermedades previas: {enfermedadesPrevias}");
            Console.WriteLine($"Tratamientos previos: {tratamientosPrevios}");
            Console.WriteLine($"Impresión diagnóstica: {impresionDiagnostica}");
            Console.WriteLine($"Usuario: {usuario}");
            Console.WriteLine($"Contraseña: {password}");
            Console.WriteLine("Exámenes de laboratorio realizados:");
            foreach (string examen in examenesLaboratorio)
            {
                Console.WriteLine($"- {examen}");
            }

            Console.Write("\n¿Desea generar el paz y salvo por los servicios médicos recibidos y facturados? (si/no): ");
            string respuesta = Console.ReadLine();
            if (respuesta.ToLower() == "si")
            {
                GenerarPazYSalvo(nombre, apellidos);
            }

            Console.WriteLine("\n¡Registro completado!");

            Console.ReadLine();

            Console.WriteLine("Informacion registrada en archivo");
            GuardarInformacion(tipoDocumento, numDocumento, nombre, apellidos, sexo, fechaNacimiento, direccion, correoElectronico, telefono, municipio, departamento, enfermedadesPrevias, tratamientosPrevios, impresionDiagnostica, usuario, password);
        }

        static void GuardarInformacion(string tipoDocumento, string numDocumento, string nombre, string apellidos, string sexo, DateTime fechaNacimiento, string direccion, string correoElectronico, string telefono, string municipio, string departamento, string enfermedadesPrevias, string tratamientosPrevios, string impresionDiagnostico, string usuario, string password)
        {
            string rutaArchivo = "pacientes.txt";
            Console.WriteLine("Ruta del archivo: " + Path.GetFullPath(rutaArchivo));

            using (StreamWriter sw = File.AppendText(rutaArchivo))
            {
                sw.WriteLine("Tipo de documento: " + tipoDocumento);
                sw.WriteLine("Número de documento: " + numDocumento);
                sw.WriteLine("Nombre: " + nombre);
                sw.WriteLine("Apellidos: " + apellidos);
                sw.WriteLine("Sexo: " + sexo);
                sw.WriteLine("Fecha de Nacimiento: " + fechaNacimiento);
                sw.WriteLine("Direccion: " + direccion);
                sw.WriteLine("Correo Electronico: " + correoElectronico);
                sw.WriteLine("Telefono: " + telefono);
                sw.WriteLine("Municipio: " + municipio);
                sw.WriteLine("Departamento: " + departamento);
                sw.WriteLine("Enfermedades previas: " + enfermedadesPrevias);
                sw.WriteLine("Tratamientos previos: " + tratamientosPrevios);
                sw.WriteLine("Impresion diagnostica: " + impresionDiagnostico);
                sw.WriteLine("Usuario: " + usuario);
                sw.WriteLine("Password: " + password);
                sw.WriteLine();
            }

            Console.WriteLine("La información del paciente se ha guardado en el archivo.");
        }

        static void LeerInformacionPacientes()
        {
            string rutaArchivo = "pacientes.txt";

            if (File.Exists(rutaArchivo))
            {
                Console.WriteLine("");
                Console.WriteLine("Leyendo información de pacientes...");
                Console.WriteLine("");

                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(linea);
                    }
                }

                Console.WriteLine("Fin de la lectura.");
            }
            else
            {
                Console.WriteLine("El archivo de pacientes no existe.");
            }
        }


        static int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime ahora = DateTime.Today;
            int edad = ahora.Year - fechaNacimiento.Year;
            if (fechaNacimiento > ahora.AddYears(-edad))
                edad--;
            return edad;
        }

        static string GenerarUsuario(string nombre, string apellidos, DateTime fechaNacimiento)
        {
            string usuario = $"{nombre.ToLower()}{apellidos.ToLower().Replace(" ", "")}{fechaNacimiento.Year}";
            return usuario;
        }

        static string GenerarPassword()
        {
            Random rnd = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] contraseña = new char[8];
            for (int i = 0; i < contraseña.Length; i++)
            {
                contraseña[i] = caracteres[rnd.Next(caracteres.Length)];
            }
            return new string(contraseña);
        }

        static Dictionary<string, List<string>> medicosPacientes = new Dictionary<string, List<string>>();

        static void AsignarPacienteAMedico(string medico, string nombre, string apellidos, string tipoDocumento, string numDocumento)
        {
            if (medicosPacientes.ContainsKey(medico))
            {
                medicosPacientes[medico].Add($"{nombre} {apellidos} ({tipoDocumento}: {numDocumento})");
            }
            else
            {
                medicosPacientes.Add(medico, new List<string> { $"{nombre} {apellidos} ({tipoDocumento}: {numDocumento})" });
            }
        }

        static void GenerarPazYSalvo(string nombre, string apellidos)
        {
            Console.WriteLine("\nPaz y salvo generado para: " + nombre + " " + apellidos);
        }
    }
}
