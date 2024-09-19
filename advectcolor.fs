#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//tex1
uniform sampler2D oldcolor;//tex0
layout(location=2)out vec3 newcolor;//tex3

void main()
{
	//float dt=2.0;
	float dt=2.0;
	//vec2 pos=vec2(gl_FragCoord.x,gl_FragCoord.y);
	//vec2 oldpos=pos-dt*1.0*vec2(texture(uv0,TexCoords));//uv0 is velocity
	//vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);
	//float sa,sb,sc;
	//float s=800*600/2.0;
	//if(oldpos.y/oldpos.x<0.75)
	//{
		//sa=((800-oldpos.x)*600/2.0)/s;
		//sb=(oldpos.y*800.0/2.0)/s;
		//sc=1.0-sa-sb;
		//oldtexcoord=vec2(0.0*sa+1.0*sc+1.0*sb,0.0*sa+0.0*sc+1.0*sb);
	//}
	//else{
		//sa=((600-oldpos.y)*800/2.0)/s;
		//sb=(oldpos.x*600.0/2.0)/s;
		//sc=1.0-sa-sb;
		//oldtexcoord=vec2(0.0*sa+0.0*sc+1.0*sb,0.0*sa+1.0*sc+1.0*sb);
	//}
	//newcolor=vec3(texture(oldcolor,oldtexcoord));

	
	vec2 v1=vec2(texture(uv0,TexCoords));// v1=velocity(p)
	vec2 pos=vec2(gl_FragCoord.x,gl_FragCoord.y);
	vec2 oldpos=pos-0.5*dt*1.0*vec2(texture(uv0,TexCoords));//find p1
	vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);
	float sa,sb,sc;
	float s=800*600/2.0;
	if(oldpos.y/oldpos.x<0.75)
	{
		sa=((800-oldpos.x)*600/2.0)/s;
		sb=(oldpos.y*800.0/2.0)/s;
		sc=1.0-sa-sb;
		oldtexcoord=vec2(0.0*sa+1.0*sc+1.0*sb,0.0*sa+0.0*sc+1.0*sb);
	}
	else{
		sa=((600-oldpos.y)*800/2.0)/s;
		sb=(oldpos.x*600.0/2.0)/s;
		sc=1.0-sa-sb;
		oldtexcoord=vec2(0.0*sa+0.0*sc+1.0*sb,0.0*sa+1.0*sc+1.0*sb);
	}
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);

	vec2 v2=vec2(texture(uv0,oldtexcoord));//v2=velocity(p1)
	//uv1=vec3(texture(olduv,TexCoords));
	//uv2=vec3(1.0,1.0,1.0)-vec3(texture(olduv,oldtexcoord));
	//vec3 cc=texture(olduv,TexCoords).rgb;
	//uv1=cc+vec3(0.001,0.001,0.001);
	oldpos=pos-0.75*dt*v2;//find p2
	if(oldpos.y/oldpos.x<0.75)
	{
		sa=((800-oldpos.x)*600/2.0)/s;
		sb=(oldpos.y*800.0/2.0)/s;
		sc=1.0-sa-sb;
		oldtexcoord=vec2(0.0*sa+1.0*sc+1.0*sb,0.0*sa+0.0*sc+1.0*sb);
	}
	else{
		sa=((600-oldpos.y)*800/2.0)/s;
		sb=(oldpos.x*600.0/2.0)/s;
		sc=1.0-sa-sb;
		oldtexcoord=vec2(0.0*sa+0.0*sc+1.0*sb,0.0*sa+1.0*sc+1.0*sb);
	}
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	vec2 v3=vec2(texture(uv0,oldtexcoord));//v3=velocity(p2)
	oldpos=pos-dt*(2.0/9.0*v1+1.0/3.0*v2+4.0/9.0*v3);
	if(oldpos.y/oldpos.x<0.75)
	{
		sa=((800-oldpos.x)*600/2.0)/s;
		sb=(oldpos.y*800.0/2.0)/s;
		sc=1.0-sa-sb;
		oldtexcoord=vec2(0.0*sa+1.0*sc+1.0*sb,0.0*sa+0.0*sc+1.0*sb);
	}
	else{
		sa=((600-oldpos.y)*800/2.0)/s;
		sb=(oldpos.x*600.0/2.0)/s;
		sc=1.0-sa-sb;
		oldtexcoord=vec2(0.0*sa+0.0*sc+1.0*sb,0.0*sa+1.0*sc+1.0*sb);
	}
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	newcolor=vec3(texture(oldcolor,oldtexcoord));

}