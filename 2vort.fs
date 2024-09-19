#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//texture1
uniform sampler2D div;//textre2 uv3
layout(location=0) out vec3 uv1;
void main()
{
	float dt=0.05;
	float crul_strength=7.0;
	vec2 uv=vec2(texture(uv0,TexCoords));
	//zuo
	vec2 oldpos=vec2(gl_FragCoord.x-1.0,gl_FragCoord.y);
	vec2 oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	float cl=texture(div,oldtexcoord).y;

	//you
	oldpos=vec2(gl_FragCoord.x+1.0,gl_FragCoord.y);
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	float cr=texture(div,oldtexcoord).y;

	//shang
	oldpos=vec2(gl_FragCoord.x,gl_FragCoord.y+1.0);
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	float ct=texture(div,oldtexcoord).y;

	//xia
	oldpos=vec2(gl_FragCoord.x,gl_FragCoord.y+1.0);
	oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
	float cb=texture(div,oldtexcoord).y;

	//central
	float cc=texture(div,TexCoords).y;

	vec2 force=normalize(vec2(abs(ct)-abs(cb),abs(cl)-abs(cr)));

	force*=crul_strength*cc*dt;
	force=vec2(min(force.x,1000.0),min(force.t,1000.0));
	uv=uv+force;
	uv1=vec3(uv,0.0);


}