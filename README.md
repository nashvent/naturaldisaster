# Natural Disasters
Proponemos un juego en realidad virtual que recrea diferentes tipos de desastres naturales, los cuales se encuentran clasificados por medio de mundos en el menú de inicio. Cada jugador podrá elegir el mundo en el cual se desarrollara un determinado desastre natural, los mundos también actúan como sala de jugadores, puesto que la conexión en red se ejecuta al ingresar a dicho mundo. 

## Reglas del juego
* La partida inicia cuando al menos se conecten 2 jugadores
* Los objetos se ubicaran de manera aleatoria.
* Desde que inicia el desastre la vida de los jugadores se ira reduciendo gradualmente.
* Los objetos deben ser transportados por los jugadores hacia la marca en el mundo.
* Cuando los objetos que sean manipulados por los jugadores, se junten se creara el nuevo objeto.
* Cuando el objeto nuevo es creado, los jugadores ganan la partida.

## Tecnologias utilizadas
* Unity con GoogleVR vs .
* Servidor Node JS.
* Móvil con giroscopio por jugador.
* CardBoard por jugador.

## Para ejecutar
* Unity version 2018.2.13f1
* SDK de Android
* Configurar Unity para VR
* Instalar Node
* Instalar npm

### Pasos
Ingresar a la carpeta
```
cd nodeServe
```
Para instalar todos los paquetes necesarios.
```
npm install
```
Para correr el servidor.

```
node index.js
```
Ejecutar Main.unity por cada jugador o instalar directamente en el celular.

## Para Interactuar en el juego
### 1) Seleccíon: 
* Al inicio del juego existe un meńu en el cualse puede elegir el tipo de desastre haciendo touch en el mundo apuntado. 
* Para seleccionar un objeto se realiza un touch en elCardboard, aśı mismo para deseleccionar el objeto.

### 2) Manipulacíon:
* Se  pueden  recoger  objetos  dentro  delescenario que son necesarios para superar el desastre natural.
* Para soltar un objeto tendŕa que usar el touch.

### 3) Navegacíon:
* El  jugador  puede  recorrer  el  escenario  omundo bajando la mirada hacia el suelo, lo cual lo transportarahacia  la  direccíon  enfocada.  
* El  jugador  puede  salir  al  meńuinicial  y  volver  a  entrar  a  las  opciones  de  meńu.  
* Para  ello debe seleccionar con el touch el objeto de salida.

### 4) Multijugador:
* Otros jugadores pueden unirse a una partida   ingresando   a   un   mundo,   para   ello   es   necesario   que est́en  conectados  a  una  red  local.
* En  el  escenario  los  jugadores  pueden  interactuar  entre  ellos  seleccionando  objetos y movíendose.

### 5) Colaboracíon:
* Para superar el desastre es necesario queambos  jugadores  construyan  un  nuevo  objeto  a  partir  de  los objetos en el escenario. 
* Cada jugador podŕa manipular un solo objeto a la vez

## Video 
Adjunto en el repositorio

