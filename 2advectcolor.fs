#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//tex1
uniform sampler2D oldcolor;//tex0
layout(location=2)out vec3 newcolor;//tex3

void main()
{
	//float dt=2.0;
	float dt=0.05;
	
	vec2 v1=vec2(texture(uv0,TexCoords));// v1=velocity(p)
	vec2 pos=vec2(gl_FragCoord.x,gl_FragCoord.y);

	vec2 oldpos=pos-0.5*dt*1.0*vec2(texture(uv0,TexCoords));//find p1
	vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);
	
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);

	vec2 v2=vec2(texture(uv0,oldtexcoord));//v2=velocity(p1)

	oldpos=pos-0.75*dt*v2;//find p2
	
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	vec2 v3=vec2(texture(uv0,oldtexcoord));//v3=velocity(p2)
	oldpos=pos-dt*(2.0/9.0*v1+1.0/3.0*v2+4.0/9.0*v3);
	
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	newcolor=vec3(texture(oldcolor,oldtexcoord));

}