#version 460 core
in vec2 TexCoords;
uniform sampler2D uv1; //tex0
out vec3 uv0;

void main()
{
	uv0=vec3(texture(uv1,TexCoords));//+vec3(0.0,0.3,0.3);//-vec3(1.5,0.0,0.0);


}