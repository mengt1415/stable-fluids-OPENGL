#version 460 core
in vec2 TexCoords;
uniform sampler2D p0;//uv0 dyefb texture2
uniform sampler2D div;//uv3 uvfb attachment1 texture1
layout(location=0) out vec3 uv1;//uv1 uvfb
// in the vboundary,we set the third cinponent of uv0 to be zero,which is the original
//answer of p in the jacob iteration
//in jacob iteration,we use the p0 in uv0.z(dyefb,GL_TEXTURE2),and the
//div stored in the uv3 of uvfb(GL_TEXTURE1),and compute the p1 and store it 
//in the uv1 of uvfb(GL_TEXTURE0,COLOR ATTACHMENT0)
void main()
{

	vec3 uv=vec3(texture(p0,TexCoords));
	vec3 oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
		vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);

		//float sa,sb,sc;
		//float s=800*600/2.0;
		//zuo
		float pl=0.0;//zuo
		oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
		//if(oldpos.y/oldpos.x<0.75)
		//{
			//sa=((800-oldpos.x)*600/2.0)/s;
			//sb=(oldpos.y*800.0/2.0)/s;
			//sc=1.0-sa-sb;
			//oldtexcoord=vec2(0.0*sa+1.0*sc+1.0*sb,0.0*sa+0.0*sc+1.0*sb);
		//}
		//else
		//{
			//sa=((600-oldpos.y)*800/2.0)/s;
			//sb=(oldpos.x*600.0/2.0)/s;
			//sc=1.0-sa-sb;
			//oldtexcoord=vec2(0.0*sa+0.0*sc+1.0*sb,0.0*sa+1.0*sc+1.0*sb);
		//}
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pl=texture(p0,oldtexcoord).z;

		float pr=0.0;//you
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
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pr=texture(p0,oldtexcoord).z;

		float pt=0.0;//shang
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
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pt=texture(p0,oldtexcoord).z;

		float pb=0.0;//xia
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
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pb=texture(p0,oldtexcoord).z;

		//float pc=uv.z;
		float sandu=texture(div,TexCoords).x;
		float pnew=(pl+pr+pt+pb-sandu)*0.25;
		//(pl+pr+pb+pt+alpha*pc)*rbeta

		//uv1=vec3(vec2(uv),pnew);
		uv1=vec3(vec2(uv),pnew);
	
}