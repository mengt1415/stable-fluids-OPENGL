#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;
layout(location=1) out vec3 div;
//dx=1m
//this shader is to compute the divergence of the velocity field,
//we take uv0 of dyefb as input(color_attachment1),and stotre the div in the
// uv3,GL_TEXTURE1 ,THE COLORattachment1 of uvfb
void main()
{
	float sandu=0.0;
	//zuo
	vec3 oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
	vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);

		//float sa,sb,sc;
		//float s=800*600/2.0;
		//zuo
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
		
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		sandu-=texture(uv0,oldtexcoord).x; //zuo

		//you
		
		oldpos=vec3(gl_FragCoord.x+1.0,gl_FragCoord.y,gl_FragCoord.z);//you
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
		//boundarycolor=-vec3(texture(uv1,oldtexcoord));
		//boundarycolor=vec3(1.0,1.0,1.0);
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		sandu+=texture(uv0,oldtexcoord).x;

		//shang
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
		//boundarycolor=vec3(1.0,1.0,1.0)-vec3(texture(uv1,oldtexcoord));//zuo
		//boundarycolor=-vec3(texture(uv1,oldtexcoord));
		//boundarycolor=vec3(1.0,1.0,1.0);
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		sandu+=texture(uv0,oldtexcoord).y;

		//xia
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
			//ldtexcoord=vec2(0.0*sa+0.0*sc+1.0*sb,0.0*sa+1.0*sc+1.0*sb);
		//}
		//boundarycolor=vec3(1.0,1.0,1.0)-vec3(texture(uv1,oldtexcoord));//zuo
		//boundarycolor=-vec3(texture(uv1,oldtexcoord));
		//boundarycolor=vec3(1.0,1.0,1.0);
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		sandu-=texture(uv0,oldtexcoord).y;

		sandu=sandu*0.5;
	//div=vec3(80.0*sandu,80.0*sandu,80.0*sandu)
	div=vec3(sandu,sandu,sandu);
	


}