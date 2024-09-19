#version 460 core
in vec2 TexCoords;
out vec3 dyeColor;

void main()  
{
	//the checkerboard dye field

	//float sines=sin(0.03*gl_FragCoord.x)*sin(0.03*gl_FragCoord.y);
	//if(sines<0)
		//dyeColor=vec3(0.0,0.0,0.0);
	//else
		//dyeColor=vec3(0.49,0.37,0.58);

	//a drop of ink

	//float d2=(gl_FragCoord.x-700.0)*(gl_FragCoord.x-700.0)+(gl_FragCoord.y-300.0)*(gl_FragCoord.y-300.0);
	//float res=500.0;
	//dyeColor=vec3(0.49,0.37,0.58)*exp(-d2*(1/((res/15)*(res/15))));
	dyeColor=vec3(0.0,0.0,0.0);
	
	//uv0=vec3(0.0,0.0,0.0); //this should be the correct original velocity field in final version

	//uv0=vec3(((gl_FragCoord.x/800.0)*0.5+(gl_FragCoord.y/600.0)*0.5)*((gl_FragCoord.x/800.0)*0.5+(gl_FragCoord.y/600.0)*0.5),0.0,0.0);
	//uv0=vec3(1.0-(gl_FragCoord.x/800.0),0.0,0.0); //test advect.fs
	//uv0=vec3(0.3,0.0,0.0);
}