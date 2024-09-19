#version 460 core
in vec2 TexCoords;
uniform sampler2D uv1;//tex1
out vec3 uv0;
void main()//dt=0.8 advect.fs force.fs advectcolor 的时间需要一致 main->mian error 3001
{
	
	vec3 uv=vec3(texture(uv1,TexCoords));
	uv0=uv;
	//uv0=uv;
}