#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//tex0
out vec3 uv1;//tex1
//the pressure is stored in the z conponent of uv0 and uv1
//uv0 is GL_TEXTURE1  UV0 IS GL_TEXTURE0 color attachment1
void main()
{
	vec3 uv=vec3(texture(uv0,TexCoords));
	//float pl,pr,pt,pb;

	//zuo pl
	vec2 xoffset=vec2(1.0/800.0,0.0);
	vec2 yoffset=vec2(0.0,1.0/800.0);

	float pl=texture(uv0,TexCoords-xoffset).z;
	float pr=texture(uv0,TexCoords+xoffset).z;
	float pt=texture(uv0,TexCoords+yoffset).z;
	float pb=texture(uv0,TexCoords-yoffset).z;

	vec2 g=vec2(0.5*(pr-pl),0.5*(pt-pb));

	uv1=vec3(g.x,g.y,0);
}