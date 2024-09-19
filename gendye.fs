#version 460 core
in vec2 TexCoords;
layout(location=0)out vec3 dyeColor;
layout(location=1) out vec3 uv0;
void main()  
{
	float sines=sin(0.03*gl_FragCoord.x)*sin(0.03*gl_FragCoord.y);
	if(sines<0)
		dyeColor=vec3(0.0,0.0,0.0);
	else
		dyeColor=vec3(0.49,0.37,0.58);
	//if(gl_FragCoord.x+gl_FragCoord.y<400)
		//uv0=vec3(0.49,0.37,0.58);
		//uv0=vec3(0.0,0.5,0.0);
	//else
		//uv0=vec3(0.0,0.0,0.0);
	
	uv0=vec3(0.0,0.0,0.0);
	//uv0=vec3(((gl_FragCoord.x/800.0)*0.5+(gl_FragCoord.y/600.0)*0.5)*((gl_FragCoord.x/800.0)*0.5+(gl_FragCoord.y/600.0)*0.5),0.0,0.0);
	//uv0=vec3((gl_FragCoord.x/800.0),0.0,0.0);
}