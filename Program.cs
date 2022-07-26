// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

//crea listas
List<Personaje> EquipoUno = new List<Personaje>();
List<Personaje> EquipoDos = new List<Personaje>();
Personaje Ganador;

System.Console.WriteLine("Para crear los personajes via json presione 1. De lo contrario se crearán aleatoriamente.");
int aux = Int16.Parse(Console.ReadLine());
if (aux == 1)
{
    do
    {
        if (File.Exists("jugadores.json"))
        {
            string textjson = File.ReadAllText("jugadores.json");
            List<Personaje> Equipos = JsonSerializer.Deserialize<List<Personaje>>(textjson);
            
            EquipoUno = Equipos.GetRange(0,5);
            EquipoDos = Equipos.GetRange(5,5);
            aux = 1;
        } else {
            System.Console.WriteLine("No se encuentra ningun archivo 'jugadores.json' en el directorio. Por favor intente nuevamente.");
            aux = 2;
        }
    } while (aux == 2);
    
} else {
    
    //crea equipo aleatoriamente
    EquipoUno = cargarEquipo(EquipoUno,"EquipoUno");
    EquipoDos = cargarEquipo(EquipoDos,"EquipoDos");
    List<Personaje> Equipos = new List<Personaje>();
    Equipos.AddRange(EquipoUno);
    Equipos.AddRange(EquipoDos);
    string textjson = JsonSerializer.Serialize(Equipos);

    if (!File.Exists("jugadores.json"))
    {
        File.Create("jugadores.json");
    }
    File.WriteAllText("jugadores.json",textjson);


}
mostrarEquipo(EquipoUno);
mostrarEquipo(EquipoDos);
//crea archivo ganadores
string nuevoArchivo = "ganadores.csv";
string cabecera = "Nombre, Equipo, Lucha contra, Fecha de combate, Rondas jugadas, Salud Final";
if (!File.Exists(nuevoArchivo))
{
    File.Create(nuevoArchivo);
    File.WriteAllText(nuevoArchivo,cabecera);
}

string arch = File.ReadAllText(nuevoArchivo);
if (string.IsNullOrEmpty(arch))
{
    File.WriteAllText(nuevoArchivo,cabecera);
}
List<string> lineas = new List<string>();

//PELEAS
while (EquipoUno.Count > 0 && EquipoDos.Count > 0)
{
    combate(EquipoUno,EquipoDos,EquipoUno[0],EquipoDos[0],lineas,nuevoArchivo);
}

//Determina el ganador
if (EquipoUno.Count == 0)
{
    Ganador = EquipoDos[0];
} else {
    Ganador = EquipoUno[0];
}



//Presentacion del ganador
System.Console.WriteLine("TENEMOS UN GANADOR! EN LA RONDA ");
mostrarDatos(Ganador);
mostrarCaracteristicas(Ganador);
File.WriteAllLines(nuevoArchivo,lineas); 

System.Console.WriteLine("Para ver los ganadores de cada partida presione 1.");
int boton = Int16.Parse(Console.ReadLine());
if (boton == 1)
{
    foreach (var linea in File.ReadLines(nuevoArchivo))
    {
        System.Console.WriteLine(linea);
    }
}





//--------------FUNCIONES-------------------



List<Personaje> cargarEquipo(List<Personaje> Lista, string nombreEquipo) {

    for (int i = 0; i < 5; i++)
    {
        Lista.Add(new Personaje{Equipo = nombreEquipo});
    }
    return Lista;
}
void mostrarEquipo(List<Personaje> Lista) {
    foreach (Personaje P in Lista)
    {
        System.Console.WriteLine("---DATOS---");
        mostrarDatos(P);
        System.Console.WriteLine("---CARACTERISTICAS---");
        mostrarCaracteristicas(P);
    }
}
void mostrarDatos(Personaje P) {
    System.Console.WriteLine("Pertenezco a " + P.Equipo);
    System.Console.WriteLine("ID: " + P.id);
    System.Console.WriteLine("Mi nombre es " + P.Nombre);
    System.Console.WriteLine("Soy " + P.Tipo);
    System.Console.WriteLine("Me dicen " + P.Apodo);
    System.Console.WriteLine("Nací el " + P.FechaNac.Date.ToShortDateString());
    System.Console.WriteLine("Por lo que tengo " + P.Edad + " años");
    System.Console.WriteLine("Mi salud está al " + P.Salud.ToString("N2") + "%");
}
void mostrarCaracteristicas(Personaje P) {
    System.Console.WriteLine("Velocidad: " + P.Velocidad + "/10");
    System.Console.WriteLine("Destreza: " + P.Destreza + "/5");
    System.Console.WriteLine("Fuerza: " + P.Fuerza  + "/10");
    System.Console.WriteLine("Nivel : " + P.Nivel + "/10");
    System.Console.WriteLine("Armadura: " + P.Armadura + "/10");
}
 
void combate(List<Personaje> ListaL,List<Personaje> Listav, Personaje L, Personaje V, List<string> Lineas, string archivo) {
    int rondas = 0;
    do
    {
        for (int i = 0; i < 3; i++)
        {
            ataque(L,V);
            ataque(V,L);
            rondas++;
        } 
    } while ((L.Salud == V.Salud) && L.Salud > 0 && V.Salud > 0);
    
    if (L.Salud > V.Salud )
    {
        agregarGanador(archivo,Lineas,L,V,rondas);
        Listav.Remove(V);
        bonusGanador(L);

    } else {
        agregarGanador(archivo,Lineas,V,L,rondas);
        ListaL.Remove(L);
        bonusGanador(V);
    }
}

void agregarGanador(string archivo, List<string> Lineas, Personaje G, Personaje P, int rondas) {
    string linea;
    linea = G.Nombre + "," + G.Equipo + "," + P.Nombre + ","+ DateTime.Now.ToString() + "," + rondas + "," + G.Salud ;
    Lineas.Add(linea);
}
void bonusGanador(Personaje P) {
        P.Salud += 5;
        P.Fuerza += new Random().Next(5,10);
        P.Armadura += new Random().Next(1,3);
        P.Destreza += new Random().Next(1,3);
        P.Velocidad += new Random().Next(1,3);
        P.Nivel += 1;
}

void ataque(Personaje L, Personaje V) {
    double PD = L.Destreza * L.Fuerza * L.Nivel;
    double ED = new Random().Next(1,100);
    double VA = PD * ED;
    double PDEF = V.Armadura * V.Velocidad;
    int MDP = new Random().Next(1,50000) ;
    double DP = (VA * ED - PDEF) / (MDP)  ;
    V.Salud -= DP;
}


//---------------CLASES----------------

//clase de Json
 public class Magos
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

    }

class Caracteristicas {
    
    public int generarRandom(int min, int max) {
        return new Random().Next(min,max);
    }  
    private int velocidad, destreza, fuerza, nivel, armadura;
    public Caracteristicas() {
        this.velocidad = generarRandom(1,10);
        this.destreza = generarRandom(1,5);
        this.fuerza = generarRandom(1,10);
        this.nivel = generarRandom(1,10);
        this.armadura = generarRandom(1,10);
    }

    [JsonPropertyName("Velocidad")]
    public int Velocidad {
        get{
            return velocidad; 
        } set {
            if (value > 10)
            {
                velocidad = 10;
            } else {
                velocidad = value;
            }
            
        }
    }

    [JsonPropertyName("Destreza")]
    public int Destreza {
        get{
            return destreza;; 
        }set {
            if (value > 5)
            {
                destreza = 5;
            } else {
                destreza = value;
            }
            
        }
    }

    [JsonPropertyName("Fuerza")]
    public int Fuerza {
        get{
            return fuerza; 
        } set {
            if (value > 10)
            {
                fuerza = 10;
            } else {
                fuerza = value;
            }
            
        }
    }

    [JsonPropertyName("Nivel")]
    public int Nivel {
        get{
            return nivel; 
        } set {
            if (value > 10)
            {
                nivel = 10;
            } else{
                nivel = value;
            }
           
        }
    }

    [JsonPropertyName("Armadura")]
    public int Armadura {
        get{
            return armadura; 
        } set {
            if (value > 10)
            {
                armadura = 10;
            } else {
                armadura = value;
            }
            
        }
    }
    
}
class Personaje:Caracteristicas {

    private static int aux;
    private int ID;
    private string []nombresPosibles = new string[17];
    private string[]tipos = {"Trikru","Azgeda","Floukru","Sankru"};
    private string nombre;
    private string tipo;
    private string equipo;
    private string apodo;
    private int dia, mes, anio;
    private DateTime fechaNac;
    private int edad;
    private double salud;
    
    
        public Personaje() {

        var url = $"https://wizard-world-api.herokuapp.com/Wizards" ;
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse()) {
                using (Stream strReader = response.GetResponseStream()) {
                    if (strReader == null) return;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        int ind = 0;
                        List<Magos> Mago  = JsonSerializer.Deserialize<List<Magos>>(responseBody);
                        
                        foreach (var item in Mago)
                        {
                            nombresPosibles[ind] = item.FirstName + " " + item.LastName;
                            ind++;
                        }
                    }
                }
            }
        }
        catch (System.Exception)
        {
            throw;
        }

        this.nombre = nombresPosibles[generarRandom(0,17)];
        this.tipo = tipos[generarRandom(0,3)];
        this.apodo = this.nombre.Substring(0,3);
        this.dia = generarRandom(1,28);
        this.mes = generarRandom(1,12);
        this.anio = generarRandom(1722,2022);
        this.fechaNac = new DateTime(anio,mes,dia);
        this.edad =  DateTime.Today.AddTicks(-this.fechaNac.Ticks).Year - 1;
        this.salud = 100;
        aux += 1;
        this.ID = aux;
    }

    [JsonPropertyName("id")]
    public int id { 
        get {
            return ID;
        } set {
            ID = value;
        }
    }

    [JsonPropertyName("Nombre")]
    public string Nombre {
        get{
            return nombre;
        } set {
            nombre = value;
        }
    }

    [JsonPropertyName("Tipo")]
    public string Tipo {
        get {
            return tipo;
        } set {
            tipo = value;
        }
    }

    [JsonPropertyName("Equipo")]
    public string Equipo {
        get {
            return equipo;
        } set {
            equipo = value;
        }
    }

    [JsonPropertyName("Apodo")]
    public string Apodo {
        get {
            return apodo;
        } set {
            apodo = value;
        }
    }

    [JsonPropertyName("FechaNac")]
    public DateTime FechaNac {
        get {
            return fechaNac;
        } set  {
            fechaNac = value;
        }
    }

    [JsonPropertyName("Salud")]
    public double Salud {
        get {
            return salud;
        } set {
            
            if (value < 0)
            {
                salud = 0;
            } else {
                if (value > 100)
                {
                    salud = 100;
                } else {
                    salud = value; 
                }
            } 
            
            
        }
    }

    [JsonPropertyName("Edad")]
    public int Edad {
        get {
            return edad;
        } set {
            edad = value;
        }
    }
}
 