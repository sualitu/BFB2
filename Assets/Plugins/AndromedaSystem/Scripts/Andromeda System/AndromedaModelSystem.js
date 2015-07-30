//Andromeda Model System JavaScript Version 1.0 @ Copyright
//Black Horizon Studios

var floatUpSpeed : float = .01; 
var floatDownSpeed : float = .01; 
var shakeIntensity : float = .1;

var hologramModel : GameObject;

var rotateSpeed : float = 0;
private var floatup;

private var flickerSpeed : float;
var minFlicker : float = 0;
var maxFlicker : float = 1;

var offsetX : float; 
var offsetY : float; 
var xSpeed : float = 1; 
var ySpeed : float = 1; 

function Start(){
    floatup = false;  
	
	if (hologramModel == null)
		Debug.LogError("You need to apply a model to the Hologram Model slot. The model must contain 1st the the Hologram Static Shader then the Hologram Solid Shader. Refer to the demo scene for an example if needed.");
}
function Update(){
   	
    offsetX = Time.time * xSpeed; 
    offsetY = Time.time * ySpeed; 

    if(floatup)
        floatingup();
    else if(!floatup)
        floatingdown();
        
    flickerSpeed = Random.Range(minFlicker,maxFlicker);

	hologramModel.GetComponent.<Renderer>().material.color.a = flickerSpeed;
	
	if (maxFlicker > 2)
	{
		Debug.LogError("Max flicker amount should not exceed 2");
	}	
	
	if (minFlicker < -1)
	{
		Debug.LogError("Min flicker amount should not go below -1");
	}
	
	hologramModel.GetComponent.<Renderer>().material.mainTextureOffset = Vector2 (offsetX,offsetY); 
        
}
function floatingup(){
    transform.position.y += shakeIntensity * Time.deltaTime;
    yield WaitForSeconds(floatUpSpeed);
    floatup = false;
}
function floatingdown(){
    transform.position.y -= shakeIntensity * Time.deltaTime;;
    yield WaitForSeconds(floatDownSpeed);
    floatup = true;
}