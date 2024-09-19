#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//tex1
uniform sampler2D oldcolor;//tex0
uniform float gridsize;
out vec3 newcolor;

void main()
{
	//float dt=2.0;
	float dt=0.02;
	
	
	vec2 v1=vec2(texture(uv0,TexCoords));
	vec2 offset=0.5*dt/gridsize*v1;
	//vec2 offset=0.5*dt/800*v1;

	vec2 v2=vec2(texture(uv0,TexCoords-offset));

	offset=0.75*dt/gridsize*v2;
	//offset=0.75*dt/800*v2;
	vec2 v3=vec2(texture(uv0,(TexCoords-offset)));

	offset=dt/gridsize*(2.0/9.0*v1+1.0/3.0*v2+4.0/9.0*v3);
	//offset=dt/800*(2.0/9.0*v1+1.0/3.0*v2+4.0/9.0*v3);
	newcolor=vec3(texture(oldcolor,TexCoords-offset))*0.999;//0.999

}