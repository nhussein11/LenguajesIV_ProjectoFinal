# Projecto Final - LenguajesIV - 2021
## Objetivo: 
  Proveer a los oficiales de tránsito de una herramienta digital en formato de aplicativo móvil que les permita almacenar las multas o infracciones que registran en su día a día en papel, posibilitando también, la oportunidad de transmitir fácilmente dicha información a sus superiores, omitiendo lentos y extensos procesos burocráticos.
## Metodología:
Para el desarrollo del sistema, se utilizará el Framework conocido como Xamarin Forms acompañado con un soporte en una base de datos administrada por el motor SQLite. Además se utilizará la API de Google Maps para trabajar la geolocalización. En el caso de ser estrictamente necesario se optara por complementar la base de datos SQLite con una instancia de Firebase/MongoDB para almacenar de manera más conveniente las imágenes de los documentos de identidad como así también la información respecto a la geolocalización
## Estructura: 
Los módulos en la aplicación serán los siguientes:
+ Un módulo que brinda al efectivo la posibilidad de rellenar un formulario con los datos del infractor,adjuntando a este la verificación de la identidad del infractor mediante una captura de su DNI o documento autorizado
+ Un módulo que permitirá al efectivo un control de sus datos personales, como así también llevar un registro de las infracciones asentadas
+ Un módulo que brinda la posibilidad de los efectivos de emitir correos electrónicos a sus superiores o casas centrales con los datos registrados
+ Un módulo de geolocalización donde el efectivo podrá registrar puntos en el mapa donde sucedieron las infracciones

