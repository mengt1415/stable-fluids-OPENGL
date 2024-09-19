#version 460 core
in vec2 TexCoords;
uniform sampler2D uv1;//tex1
uniform float lx;
uniform float ly;
uniform float ox;
uniform float oy;
out vec3 uv0;
void main()//dt=0.8 advect.fs force.fs advectcolor 的时间需要一致 main->mian error 3001
{
	float dt=0.05;
	float force_radius=800.0;
	float f_strength=1000;
	//float y=-(gl_FragCoord.y-300.0);
	//float x=gl_FragCoord.x-400.0;
	//float maxdix=500;
	float y=0.0;
	float x=1.0;
	float d22=(gl_FragCoord.x-lx)*(gl_FragCoord.x-lx)+(gl_FragCoord.y-800+ly)*(gl_FragCoord.y-800+ly);
	vec2 forcedir2=normalize(vec2(lx-ox,-(ly-oy)));//xuan zhuan
	float d2=(gl_FragCoord.x-100)*(gl_FragCoord.x-100)+(gl_FragCoord.y-400)*(gl_FragCoord.y-400);
	vec2 forcedir=normalize(vec2(x,y));//xuan zhuan
	//vec3 force=vec3(gl_FragCoord.y/600,0.0,0.0);
	//float dis=pow(x*x+y*y,0.5)+0.3;
	//float c=pow(dis/500.0,9);
	//float c=0.015;
	//float c=0.015;
	float factor=exp(-(d22/force_radius));
	//vec3 f=vec3(c*force.x*dt,c*force.y*dt,0.0);//f*t
	vec2 fdt=forcedir2*f_strength*factor*dt;
	vec3 uv=vec3(texture(uv1,TexCoords));
	uv0=uv+vec3(fdt,0.0);//set the p0 for pressure jacob iteration
	//uv0=uv;
}