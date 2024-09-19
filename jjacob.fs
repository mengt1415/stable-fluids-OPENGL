#version 460 core
in vec2 TexCoords;
uniform sampler2D p0;//uv1  tex2    p is sotred in uv0.z
uniform sampler2D div;//uv2  tex1
out vec3 uv1;//uv3 uvfb


//非常奇怪，这里会影响到uv3 of dyefb；
void main()
{

	vec2 xoffset=vec2(1.0/800,0.0);
	vec2 yoffset=vec2(0.0,1.0/800);
	float sandu=texture(div,TexCoords).x;
	float pl=texture(p0,TexCoords-xoffset).z;
	float pr=texture(p0,TexCoords+xoffset).z;
	float pt=texture(p0,TexCoords+yoffset).z;
	float pb=texture(p0,TexCoords-yoffset).z;

	float pnew=(pl+pr+pt+pb-sandu)*0.25;
	uv1=vec3(0.0,0.0,pnew);
	
}