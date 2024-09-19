#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//tex0
layout(location=0) out vec3 uv1;//tex1
//the pressure is stored in the z conponent of uv0 and uv1
//uv0 is GL_TEXTURE1  UV0 IS GL_TEXTURE0 color attachment1
void main()
{
	vec3 uv=vec3(texture(uv0,TexCoords));
	float pl,pr,pt,pb;

	//zuo pl
	vec3 oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
	vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);

	    
		oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
	
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pl=texture(uv0,oldtexcoord).z; //zuo

		//you pr
		oldpos=vec3(gl_FragCoord.x+1.0,gl_FragCoord.y,gl_FragCoord.z);
		
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pr=texture(uv0,oldtexcoord).z; //you

		//shang pt
		oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y+1.0,gl_FragCoord.z);
	
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pt=texture(uv0,oldtexcoord).z; //shang

		//xia pb
		oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y-1.0,gl_FragCoord.z);
	
		oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		pt=texture(uv0,oldtexcoord).z; //xia
		
		vec2 g=vec2(0.5*(pr-pl),0.5*(pt-pb));
		//uv1=vec3(g,uv.z);
		uv1=vec3(0.0,g.y,uv.z);
}