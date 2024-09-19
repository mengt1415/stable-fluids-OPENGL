#version 460 core
in vec2 TexCoords;
uniform sampler2D uv1;
layout(location=1) out vec3 boundarycolor;
// this shader is to maintain the boundary condition of the
// velocity field,after the advect wat applied, we take the advect velocity uv1
// as input (GL_TEXTURE0) ,and store the output back to uv0,the color attachment1 of dyefb
void main()
{
	
    //this fs shader will also set the initial pressure
	//for the jacob iteration in the z conponent

	boundarycolor=vec3(texture(uv1,TexCoords));
	vec3 uv=boundarycolor;
	vec3 temp=vec3(1.0,1.0,1.0);
	vec3 oldpos=vec3(gl_FragCoord.x+1.0,gl_FragCoord.y,gl_FragCoord.z);
	if(gl_FragCoord.x==0.5)
	{
		vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);
		//float sa,sb,sc;
		//float s=800*600/2.0;
		oldpos=vec3(gl_FragCoord.x+1.0,gl_FragCoord.y,gl_FragCoord.z);
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
		//boundarycolor=vec3(1.0,1.0,1.0)-vec3(texture(uv1,oldtexcoord));//zuo
		//boundarycolor=-1.0*vec3(texture(uv1,oldtexcoord));
		//boundarycolor=vec3(1.0,1.0,1.0);
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		temp=vec3(texture(uv1,oldtexcoord));
		boundarycolor=vec3(-temp.x,uv.y,uv.z);
	}
	else if(gl_FragCoord.x==799.5)
	{
		vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);
		//float sa,sb,sc;
		//float s=800*600/2.0;
		oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
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
		//boundarycolor=vec3(1.0,1.0,1.0)-vec3(texture(uv1,oldtexcoord));//you
		//boundarycolor=-1.0*vec3(texture(uv1,oldtexcoord));
		//boundarycolor=vec3(0.0,1.0,0.0);
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		temp=vec3(texture(uv1,oldtexcoord));
		boundarycolor=vec3(-temp.x,uv.y,uv.z);
	}
	else if(gl_FragCoord.y==0.5)//xia mian
	{
		vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);
		//float sa,sb,sc;
		//float s=800*600/2.0;
		oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y+1.0,gl_FragCoord.z);
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
		//boundarycolor=vec3(1.0,1.0,1.0)-vec3(texture(uv1,oldtexcoord));//xia
		//boundarycolor=-1.0*vec3(texture(uv1,oldtexcoord));
		//boundarycolor=vec3(0.0,0.0,1.0);
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		temp=vec3(texture(uv1,oldtexcoord));
		
		boundarycolor=vec3(uv.x,-temp.y,uv.z);
	}
	else if(gl_FragCoord.y==599.5)
	{
		vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);
		//float sa,sb,sc;
		//float s=800*600/2.0;
		oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y-1.0,gl_FragCoord.z);
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
		//boundarycolor=vec3(1.0,1.0,1.0)-vec3(texture(uv1,oldtexcoord));//shang
		//boundarycolor=-1.0*vec3(texture(uv1,oldtexcoord));
		//boundarycolor=vec3(1.0,1.0,0.0);
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		temp=vec3(texture(uv1,oldtexcoord));
		boundarycolor=vec3(uv.x,-temp.y,uv.z);
	}

	//boundarycolor=vec3(texture(uv1,TexCoords));
}