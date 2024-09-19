#version 460 core
in vec2 TexCoords;
out vec3 uv0;

void main()  
{
	
	uv0=vec3(0.0,0.0,0.0); //this should be the correct original velocity field in final version

	//uv0=vec3(((gl_FragCoord.x/800.0)*0.5+(gl_FragCoord.y/600.0)*0.5)*((gl_FragCoord.x/800.0)*0.5+(gl_FragCoord.y/600.0)*0.5),0.0,0.0);
	//uv0=vec3((1.0-(gl_FragCoord.x/800.0))*(1.0-(gl_FragCoord.x/800.0)),0.0,0.0); //test advect.fs
	//uv0=vec3((1.0-(gl_FragCoord.x/800.0))*(1.0-(gl_FragCoord.x/800.0))*(1.0-(gl_FragCoord.x/800.0))+0.01,0.0,0.0);
	//uv0=vec3((1.0-(gl_FragCoord.x/800.0)),0.0,0.0);
	//uv0=vec3(((gl_FragCoord.x/800.0)),0.0,0.0);
	//float d2=(gl_FragCoord.x-600.0)*(gl_FragCoord.x-600.0)+(gl_FragCoord.y-300.0)*(gl_FragCoord.y-300.0);
	//float res=500.0;
	//uv0=vec3(1.0,0,0)*exp(-d2*(1/((res/15)*(res/15))));
}