#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//texture2  uv3
uniform sampler2D div;//textre1 uv2
uniform float gridsize;
out vec3 uv1;  // uv0
void main()
{
	float dt=0.02;
	float crul_strength=3.0;// 3.0 5.0 0.0001  13
	vec2 uv=vec2(texture(uv0,TexCoords));
	//vec2 originaluv=vec2(texture(uv0,TexCoords));
	//zuo
	vec2 oldpos=vec2(gl_FragCoord.x-1.0,gl_FragCoord.y);
	vec2 oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
	//vec2 oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
	float cl=texture(div,oldtexcoord).y;

	//you
	oldpos=vec2(gl_FragCoord.x+1.0,gl_FragCoord.y);
	oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
	//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
	float cr=texture(div,oldtexcoord).y;

	//shang
	oldpos=vec2(gl_FragCoord.x,gl_FragCoord.y+1.0);
	oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
	//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
	float ct=texture(div,oldtexcoord).y;

	//xia
	oldpos=vec2(gl_FragCoord.x,gl_FragCoord.y-1.0);
	oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
	//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
	float cb=texture(div,oldtexcoord).y;

	//central
	float cc=texture(div,TexCoords).y;

	//way 1  enhance vorticity
	//vec2 force=normalize(vec2(abs(ct)-abs(cb),abs(cl)-abs(cr)));
	//force=force*crul_strength*cc;
	//force=vec2(min(force.x,1000.0),min(force.y,1000.0));
	//uv=uv+force*dt;
	//uv1=vec3(uv,0.0);
	//uv1=vec3(texture(uv0,TexCoords));
	vec2 force=vec2(abs(ct)-abs(cb),abs(cl)-abs(cr));
	force/=length(force)+0.0001;
	//float lensqure=max(0.001,dot(force,force));
	force*=crul_strength*cc;
	uv=uv+force*dt;
	uv=min(max(uv,-1000),1000);
	uv1=vec3(uv,0.0);
	//uv1=vec3(texture(uv0,TexCoords));


}