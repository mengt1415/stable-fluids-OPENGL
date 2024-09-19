#version 460 core
in vec2 TexCoords;
out vec3 dyeColor;
void main()
{
	dyeColor=vec3(0.0,0.0,0.0);
	if(gl_FragCoord.x==0.5)
		dyeColor=vec3(0.3,0.0,0.0);
	if(gl_FragCoord.x==799.5)
		dyeColor=vec3(-0.3,0.0,0.0);
}