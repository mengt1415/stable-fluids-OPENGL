#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;
layout(location=0) out vec3 uv1;
void main()//dt=0.8 advect.fs force.fs 的时间需要一致 main->mian error 3001
{
	float dt=0.04;
	//float y=-(gl_FragCoord.y-300.0);
	//float x=gl_FragCoord.x-400.0;
	//float maxdix=500;
	float y=1.0;
	float x=-1.0;
	vec3 force=normalize(vec3(y,x,0));//xuan zhuan
	//vec3 force=vec3(gl_FragCoord.y/600,0.0,0.0);
	//float dis=pow(x*x+y*y,0.5)+0.3;
	//float c=pow(dis/500.0,9);
	//float c=0.015;
	float c=0.015;

	vec3 f=vec3(c*force.x*dt,c*force.y*dt,0.0);//f*t
	vec3 uv=vec3(texture(uv0,TexCoords));
	uv1=uv+f;
}
