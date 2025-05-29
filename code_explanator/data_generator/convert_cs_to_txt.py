import os

def extract_cs_files(source_dir, output_dir):
    if not os.path.exists(output_dir):
        os.makedirs(output_dir)

    for root, _, files in os.walk(source_dir):
        for file in files:
            if file.endswith(".cs"):
                full_path = os.path.join(root, file)
                with open(full_path, 'r', encoding='utf-8', errors='ignore') as f:
                    content = f.read()

                relative_path = os.path.relpath(full_path, source_dir)
                output_file_name = relative_path.replace(os.sep, '_') + ".txt"
                output_file_path = os.path.join(output_dir, output_file_name)

                with open(output_file_path, 'w', encoding='utf-8') as out_f:
                    out_f.write(f"// Original Path: {full_path}\n\n")
                    out_f.write(content)

    print(f"Extraction complete. Files saved in: {output_dir}")

# Example usage:
extract_cs_files("code_explanator/LLD-Questions-master", "code_explanator/data_generator/code_snippets")
