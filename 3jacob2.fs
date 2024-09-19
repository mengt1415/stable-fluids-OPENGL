#version 460 core
in vec2 TexCoords;
uniform sampler2D p0;//uv1 tex2 p is sotred in uv0.z
uniform float gridsize;
uniform sampler2D div;//uv2 tex1
out vec3 uv1;//uv3 uvfb
// in the vboundary,we set the third cinponent of uv0 to be zero,which is the original
//answer of p in the jacob iteration
//in jacob iteration,we use the p0 in uv0.z(dyefb,GL_TEXTURE2),and the
//div stored in the uv3 of uvfb(GL_TEXTURE1),and compute the p1 and store it 
//in the uv1 of uvfb(GL_TEXTURE0,COLOR ATTACHMENT0)

//非常奇怪，这里会影响到uv3 of dyefb；
void main()
{

	vec3 uv=vec3(texture(p0,TexCoords));
	    //vec3 oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
		//vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);

		//zuo
		//float pl=0.0;//zuo
		//oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
	
		//oldtexcoord=vec2(oldpos.x/799.5,oldpos.y/599.5);
		//oldtexcoord=vec2(oldpos.x/800,oldpos.y/600);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		//pl=texture(p0,oldtexcoord).z;

		//you
		//float pr=0.0;//you
		//oldpos=vec3(gl_FragCoord.x+1.0,gl_FragCoord.y,gl_FragCoord.z);
	
		//oldtexcoord=vec2(oldpos.x/799.5,oldpos.y/599.5);
		//oldtexcoord=vec2(oldpos.x/800,oldpos.y/600);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		//pr=texture(p0,oldtexcoord).z;
		//if(gl_FragCoord.x+1>799.0)
			//pr=0.8;

		//shang
		//float pt=0.0;//shang
		//oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y+1.0,gl_FragCoord.z);
	
		//oldtexcoord=vec2(oldpos.x/799.5,oldpos.y/599.5);
		//oldtexcoord=vec2(oldpos.x/800,oldpos.y/600);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		//pt=texture(p0,oldtexcoord).z;

		//xia
		//float pb=0.0;//xia
		//oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y-1.0,gl_FragCoord.z);
	
		//oldtexcoord=vec2(oldpos.x/799.5,oldpos.y/599.5);
		//oldtexcoord=vec2(oldpos.x/800,oldpos.y/600);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		//pb=texture(p0,oldtexcoord).z;


	//float sandu=texture(div,TexCoords).x;
	vec2 xoffset=vec2(1.0/gridsize,0.0);
	vec2 yoffset=vec2(0.0,1.0/gridsize);
	//vec2 xoffset=vec2(1.0/800,0.0);
	//vec2 yoffset=vec2(0.0,1.0/800);
	float sandu=texture(div,TexCoords).x;
	float pl=texture(p0,TexCoords-xoffset).z;
	float pr=texture(p0,TexCoords+xoffset).z;
	float pt=texture(p0,TexCoords+yoffset).z;
	float pb=texture(p0,TexCoords-yoffset).z;
	float pnew=(pl+pr+pt+pb-sandu)*0.25;
		//if(gl_FragCoord.x==799.5)
			//pnew=0.4;

	uv1=vec3(vec2(uv),pnew);
	
}