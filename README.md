# TecAPI
 API de la TecApp

## Modelo de datos
Para cada tabla se genero un modelo, se encuentran en la carpeta models, en casos de error, avisen para ver si se corrige del lado del back end o de la base de datos.
	>Se le puede menear sin pedos a los suyos<
	>Al modelo de usuarios NO LE MENEEN<

## Controladores
No hay un controlador por cada modelo, se genero un controlador para cada tabla primaria, en caso de que el numero de operaciones les haga muy grande el controlador,
Hagan otro controlador para la misma tabla, pero con otro nombre, por ejemplo, si el controlador de Avisos se vuelve muy grande, separen las operaciones por tabla
y creen un controlador secundario con el nombre de la tabla y el sufijo Controller, por ejemplo AvisosController.
	>Se le puede menear sin pedos a los suyos<

### Rutas
El enrutamiento y los endpoints se crean con la siguiente estructura:
 [Route("[controller]")] - .Net interpreta que el nombre del controlador es el nombre de la clase sin el sufijo Controller. 
							Si el controlador se llama AlimentosController, la ruta base es /Alimentos
 [Route("GetAllAlimentos")] - Se agrega a la ruta base, en este caso, la ruta completa es /Alimentos/GetAllAlimentos
 Despues del enrutamiento se agrega el tipo de petición:
 [HttpGet] - Indica que el endpoint es de tipo GET, osea para obtener datos
 [HttpPost] - Indica que el endpoint es de tipo POST, osea para enviar datos
 [HttpPut] - Indica que el endpoint es de tipo PUT, osea para actualizar datos
 [HttpDelete] - Indica que el endpoint es de tipo DELETE, osea para eliminar datos

## Contexto
El contexto es la clase que se encarga de la conexión con la base de datos, no lo tienten por que si le mueven algo sin saberle le pueden tronar la chamba a los demas.
Si se necesita un cambio en las configuraciones de contexto, pidanselas a alguien que sepa, no lo hagan ustedes.
	>NO LE MENEEN<

## _context
Es la variable que se encarga de las operaciones con la base de datos, no le cambien el tipo por que si no le saben, se pueden desmadrar su controlador.
Si tienen dudas con las operaciones, chat gpt le sabe bien a esa madre (tambien pregunten si quieren)

## Program.cs
Lo mismo que el contexto, no le muevan ni de pedo.
Si necesitan añadir un servicio, mencionenlo a alguien que sepa, no lo hagan ustedes.
      >Para los que ocupan LogIn y FCM, Tenemos que ver ese pedo luego
	   NO LE MENEEN<
