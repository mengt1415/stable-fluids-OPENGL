#version 460 core
in vec2 TexCoords;
uniform sampler2D olduv;
layout(location=0)out vec3 uv1;
//layout(location=1) out vec3 uv2
//timestep 0.1  rdx 1
//this advect shader is to advect velocity
//take uv0 GL_TEXTURE0 as input (old uv) 
//and store the output in uv1, the color_attachment0 of uvfb
//dt=0.8 advect.fs force.fs 的时间需要一致
void main()
{
	float dt=0.040;//0.04
	vec2 v1=vec2(texture(olduv,TexCoords));// v1=velocity(p)
	vec2 pos=vec2(gl_FragCoord.x,gl_FragCoord.y);
	vec2 oldpos=pos-0.5*dt*1.0*vec2(texture(olduv,TexCoords));//find p1
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

	//vec2 oldtexcoord=vec2((oldpos.x-0.5)/799,(oldpos.y-0.5)/599.0);
	//if(oldtexcoord.y-TexCoords.y>0.0)
	    //uv1=vec3(1.0,1.0,0.0);
	//else
		//uv1=vec3(texture(olduv,TexCoords));
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	vec2 v2=vec2(texture(olduv,oldtexcoord));//v2=velocity(p1)
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
	vec2 v3=vec2(texture(olduv,oldtexcoord));//v3=velocity(p2)
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
	uv1=vec3(texture(olduv,oldtexcoord));
}
